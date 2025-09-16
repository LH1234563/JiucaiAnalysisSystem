using Dm.util;

namespace JiucaiAnalysisSystem.Common.Utilities;

public class TradeDateDecoder
{
    private const long DAY_MILLISECONDS = 86400000;
    private const int BASE_DAYS = 7657;
    private const int MASK = ~(3 << 30);
    private const int FLAG = 1 << 30;
    private static readonly int[] P = { 0, 3, 5, 6, 9, 10, 12, 15, 17, 18, 20, 23, 24, 27, 29, 30 };

    private string input;
    private int[] indices;
    private int index;
    private int bitIndex;
    private State state;
    private int s;

    private class State
    {
        public int d;
        public int p;
        public int ld;
        public int cd;
        public int c;
        public int m;
        public double pc;
        public int cp;
        public int da;
        public long sa;
        public long sv;
        public int la, lp, lv, tv, rv, zv, pp;
        public int[] cv = new int[2];
        public int md, mv;
        public int l;
        public int f;
        public int[] dv = new int[0];
        public int[] dl = new int[0];
        public int u_d, u_v, x_d, x_v;
    }

    public class KLineData
    {
        public DateTime Date { get; set; }
        public double Open { get; set; }
        public double High { get; set; }
        public double Low { get; set; }
        public double Close { get; set; }
        public long Volume { get; set; }
    }

    public class SimpleData
    {
        public DateTime Day { get; set; }
        public double Close { get; set; }
        public double PrevClose { get; set; }
    }

    public class TickData
    {
        public long Volume { get; set; }
        public double Price { get; set; }
        public double AvgPrice { get; set; }
    }

    public TradeDateDecoder()
    {
        state = new State();
        state.cv = new int[2];
    }

    public object Decode(string input)
    {
        this.input = input;
        Initialize();

        int[] header = ReadBits(new[] { 12, 6 });
        s = 63 ^ header[1];

        switch (header[0])
        {
            case 1479: return DecodeKLine();
            case 136: return DecodeTicks();
            case 200: return DecodeSimple();
            case 139: return DecodeDateList();
            case 197: return DecodeMatrix();
            default: return new List<object>();
        }
    }

    private void Initialize()
    {
        string chars = "";
        for (int i = 0; i < 64; i++)
        {
            if (i < 26) chars += (char)(i + 65);
            else if (i < 52) chars += (char)(i + 97 - 26);
            else if (i < 62) chars += (char)(i + 48 - 52);
            else if (i == 62) chars += "+";
            else chars += "/";
        }

        indices = new int[input.Length];
        for (int i = 0; i < input.Length; i++)
        {
            indices[i] = chars.IndexOf(input[i]);
        }

        index = 0;
        bitIndex = 0;
    }

    private int[] ReadBits(int[] lengths, int[] signed = null, int[] scale = null)
    {
        if (signed == null) signed = new int[lengths.Length];
        if (scale == null) scale = new int[lengths.Length];

        int[] result = new int[lengths.Length];

        for (int i = 0; i < lengths.Length; i++)
        {
            if (lengths[i] <= 0)
            {
                result[i] = 0;
                continue;
            }

            if (lengths[i] <= 30)
            {
                result[i] = ReadBits(lengths[i]);
                if (signed[i] != 0 && result[i] >= (1 << (lengths[i] - 1)))
                {
                    result[i] -= (1 << lengths[i]);
                }
            }
            else
            {
                int[] parts = ReadBits(new[] { 30, lengths[i] - 30 }, new[] { 0, signed[i] });
                result[i] = scale[i] != 0 ? parts[0] + parts[1] * (1 << 30) : parts[0] + parts[1] * (1 << 30);
            }
        }

        return result;
    }

    private int ReadBits(int length)
    {
        if (index >= indices.Length) return 0;

        int value = 0;
        int remaining = length;

        while (remaining > 0)
        {
            if (index >= indices.Length) break;

            int available = 6 - bitIndex;
            int take = Math.Min(available, remaining);

            int mask = (1 << take) - 1;
            int bits = (indices[index] >> bitIndex) & mask;
            value |= bits << (length - remaining);

            bitIndex += take;
            if (bitIndex >= 6)
            {
                bitIndex -= 6;
                index++;
            }

            remaining -= take;
        }

        return value;
    }

    private bool ReadBit()
    {
        if (index >= indices.Length) return false;

        bool bit = (indices[index] & (1 << bitIndex)) != 0;
        bitIndex++;

        if (bitIndex >= 6)
        {
            bitIndex -= 6;
            index++;
        }

        return bit;
    }

    private int ReadVariable()
    {
        int t = ReadBits(1);
        int e = 1;

        while (true)
        {
            if (!ReadBit()) return e * (2 * t - 1);
            e++;
        }
    }

    private DateTime GetDate(int days)
    {
        for (int i = 0; i < days; i++)
        {
            state.d++;
            int weekday = state.d % 7;
            if (weekday == 3 || weekday == 4)
            {
                state.d += 5 - weekday;
            }
        }

        DateTime date = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        return date.AddMilliseconds((BASE_DAYS + state.d) * DAY_MILLISECONDS);
    }

    private List<KLineData> DecodeKLine()
    {
        if (s >= 1) return new List<KLineData>();

        state.lv = 0;
        state.ld = 0;
        state.cd = 0;
        state.cv[0] = 0;
        state.cv[1] = 0;
        state.p = ReadBits(6);
        state.d = ReadBits(new[] { 18 }, new[] { 1 })[0] - 1;
        state.m = (int)Math.Pow(10, state.p);

        int[] md_mv = ReadBits(new[] { 3, 3 });
        state.md = md_mv[0];
        state.mv = md_mv[1];

        List<KLineData> result = new List<KLineData>();

        while (true)
        {
            int[] c = ReadBits(new[] { 6 });
            if (c.Length == 0 || index >= indices.Length) break;

            var kline = new KLineData();
            int d = 1;

            if ((c[0] & 32) != 0)
            {
                while (true)
                {
                    int a = ReadBits(6);
                    if ((16 | a) == 63)
                    {
                        string l = (a & 16) != 0 ? "x" : "u";
                        int[] xy = ReadBits(new[] { 3, 3 });
                        if (l == "x")
                        {
                            state.x_d = xy[0] + state.md;
                            state.x_v = xy[1] + state.mv;
                        }
                        else
                        {
                            state.u_d = xy[0] + state.md;
                            state.u_v = xy[1] + state.mv;
                        }

                        break;
                    }
                    else if ((a & 32) != 0)
                    {
                        string o = (a & 8) != 0 ? "d" : "v";
                        string l = (a & 16) != 0 ? "x" : "u";
                        if (o == "d")
                        {
                            if (l == "x") state.x_d = (a & 7) + state.md;
                            else state.u_d = (a & 7) + state.md;
                        }
                        else
                        {
                            if (l == "x") state.x_v = (a & 7) + state.mv;
                            else state.u_v = (a & 7) + state.mv;
                        }
                    }
                    else
                    {
                        int o2 = a & 15;
                        if (o2 == 0) d = ReadBits(6);
                        else if (o2 == 1)
                        {
                            state.d = ReadBits(18);
                            d = 0;
                        }
                        else d = o2;

                        if ((a & 16) == 0) break;
                    }
                }
            }

            kline.Date = GetDate(d);

            int[] l_l = new int[5];
            for (int j = 0; j < 4; j++) l_l[j] = state.u_d;
            l_l[4] = state.u_v;

            int ll = P[c[0] & 15];
            if ((state.u_v & 1) != 0) ll = 31 - ll;
            if ((c[0] & 16) != 0) l_l[4] += 2;

            for (int e = 0; e < 5; e++)
            {
                if ((ll & (1 << (4 - e))) != 0) l_l[e]++;
                l_l[e] *= 3;
            }

            int[] d_v = ReadBits(l_l, new[] { 1, 0, 0, 1, 1 }, new[] { 0, 0, 0, 0, 1 });

            int o3 = state.cd + d_v[0];
            kline.Open = (double)o3 / state.m;
            kline.High = (double)(o3 + d_v[1]) / state.m;
            kline.Low = (double)(o3 - d_v[2]) / state.m;
            kline.Close = (double)(o3 + d_v[3]) / state.m;

            int a2 = d_v[4];
            state.cd = o3 + d_v[3];

            long l2 = state.cv[0] + a2;
            state.cv[0] = (int)(l2 & MASK);
            state.cv[1] = state.cv[1] + (a2 >= 0 ? 0 : -1) + (((state.cv[0] & MASK) + (a2 & MASK) & FLAG) != 0 ? 1 : 0);

            kline.Volume = (state.cv[0] & (FLAG - 1)) + state.cv[1] * FLAG;

            result.Add(kline);

            if (index >= indices.Length) break;
        }

        return result;
    }

    private List<SimpleData> DecodeSimple()
    {
        if (s >= 1) return new List<SimpleData>();

        state.d = ReadBits(new[] { 18 }, new[] { 1 })[0] - 1;
        int[] a = ReadBits(new[] { 3, 3, 30, 6 });
        state.p = a[0];
        state.ld = a[1];
        state.cd = a[2];
        state.c = a[3];
        state.m = (int)Math.Pow(10, state.p);
        state.pc = (double)state.cd / state.m;

        List<SimpleData> result = new List<SimpleData>();

        for (int t = 0;; t++)
        {
            int d = 1;
            if (ReadBit())
            {
                int[] a2 = ReadBits(new[] { 3 });
                if (a2[0] == 0) d = ReadBits(new[] { 6 })[0];
                else if (a2[0] == 1)
                {
                    state.d = ReadBits(new[] { 18 }, new[] { 1 })[0];
                    d = 0;
                }
                else d = a2[0];
            }

            var simple = new SimpleData
            {
                Day = GetDate(d)
            };

            if (ReadBit()) state.ld += ReadVariable();
            int[] a3 = ReadBits(new[] { 3 * state.ld }, new[] { 1 });
            state.cd += a3[0];
            simple.Close = (double)state.cd / state.m;

            result.Add(simple);

            if (index >= indices.Length || (index == indices.Length - 1 && ((state.c ^ (t + 1)) & 63) != 0))
                break;
        }

        if (result.Count > 0)
            result[0].PrevClose = state.pc;

        return result;
    }

    private List<TickData> DecodeTicks()
    {
        if (s > 2) return new List<TickData>();

        state.d = ReadBits(new[] { 18 }, new[] { 1 })[0] - 1;
        GetDate(1);

        int[] a = ReadBits(s < 1 ? new[] { 3, 3, 4, 1, 1, 1, 5 } : new[] { 4, 4, 4, 1, 1, 1, 3 });
        state.la = a[0];
        state.lp = a[1];
        state.lv = a[2];
        state.tv = a[3];
        state.rv = a[4];
        state.zv = a[5];
        state.pp = a[6];

        state.m = (int)Math.Pow(10, state.pp);

        if (s >= 1)
        {
            int[] a2 = ReadBits(new[] { 3, 3 });
            state.c = a2[0];
        }
        else
        {
            state.c = 2;
        }

        state.pc = ReadBits(new[] { 6 * (s >= 1 ? a[0] : 5) })[0];
        state.cp = (int)state.pc;
        state.da = 0;
        state.sa = 0;
        state.sv = 0;

        List<TickData> result = new List<TickData>();

        for (int t = 0;; t++)
        {
            var tick = new TickData();
            int f = state.tv != 0 ? (ReadBit() ? 1 : 0) : 1;

            for (int i = 0; i < 3; i++)
            {
                string p = new[] { "v", "p", "a" }[i];
                if ((f != 0 || i > 0) && ReadBit())
                {
                    int a2 = ReadVariable();
                    if (p == "v") state.lv += a2;
                    else if (p == "p") state.lp += a2;
                    else state.la += a2;
                }

                int u = p == "v" && state.rv != 0 ? (ReadBit() ? 1 : 0) : 1;
                int a3 = ReadBits(
                    new[]
                    {
                        3 * (p == "v" ? state.lv : p == "p" ? state.lp : state.la) +
                        (p == "v" && state.rv != 0 ? 7 * u : 0)
                    },
                    new[] { i > 0 ? 1 : 0 })[0] * (u != 0 ? 1 : 100);

                if (p == "v") tick.Volume = a3;
                else if (p == "p") tick.Price = (double)(state.cp += a3) / state.m;

                if (p == "v" && a3 == 0 && (s > 1 || t < 241) && (state.zv != 0 ? !ReadBit() : true))
                {
                    break;
                }
            }

            state.sv += tick.Volume;
            tick.Price = (double)state.cp / state.m;
            state.sa += tick.Volume * state.cp;
            tick.AvgPrice = (state.sv != 0)
                ? ((int)((state.sa * (2000.0 / state.m) + state.sv) / state.sv) >> 1) + state.da / 1000.0
                : tick.Price + state.da / 1000.0;

            result.Add(tick);

            if (index >= indices.Length || (index == indices.Length - 1 && ((state.c ^ t) & 7) != 0))
                break;
        }

        return result;
    }

    private List<DateTime> DecodeDateList()
    {
        if (s > 1) return new List<DateTime>();

        state.l = 0;
        int n = -1;
        state.d = ReadBits(new[] { 18 })[0] - 1;
        int i = ReadBits(new[] { 18 })[0];

        List<DateTime> result = new List<DateTime>();

        while (state.d < i)
        {
            DateTime date = GetDate(1);
            if (date > DateTime.Now)
            {
                break;
            }

            if (n <= 0)
            {
                if (ReadBit()) state.l += ReadVariable();
                n = ReadBits(new[] { 3 * state.l }, new[] { 0 })[0] + 1;
                if (result.Count == 0)
                {
                    result.Add(date);
                    n--;
                }
            }
            else
            {
                result.Add(date);
            }

            n--;
        }

        result.add(new DateTime(1992, 5, 4));
        result.Sort();
        return result;
    }

    private List<int[]> DecodeMatrix()
    {
        if (s >= 1) return new List<int[]>();

        state.f = ReadBits(new[] { 6 })[0];
        state.c = ReadBits(new[] { 6 })[0];

        state.dv = new int[state.f];
        state.dl = new int[state.f];

        for (int t = 0; t < state.f; t++)
        {
            state.dv[t] = 0;
            state.dl[t] = 0;
        }

        List<int[]> result = new List<int[]>();

        for (int t = 0;; t++)
        {
            int[] row = new int[state.f];

            for (int i = 0; i < state.f; i++)
            {
                if (ReadBit()) state.dl[i] += ReadVariable();
                state.dv[i] += ReadBits(new[] { 3 * state.dl[i] }, new[] { 1 })[0];
                row[i] = state.dv[i];
            }

            result.Add(row);

            if (index >= indices.Length || (index == indices.Length - 1 && ((state.c ^ t) & 7) != 0))
                break;
        }

        return result;
    }
}