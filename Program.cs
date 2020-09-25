using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Numerics;

namespace cstripped
{
    class Program
    {
        static void Main(string[] args)
        {
            //var p = BigInteger.Parse("115792089237316195423570985008687907853269984665640564039457584007908834671663");
            //var b = (BigInteger)7;
            //var a = BigInteger.Zero;
            var Gx = BigInteger.Parse("55066263022277343669578718895168534326250603453777594175500187360389116729240");
            var Gy = BigInteger.Parse("32670510020758816978083085130507043184471273380659243275938904335757337482424");
            var Gx1 = BigInteger.Parse("75180862758995183245141392260426345444264403731066658552825576390223923208439");
            var Gy1 = BigInteger.Parse("21885678977978804490012742746158129500179828242668934362874212623347241335110");
            var Gx2 = BigInteger.Parse("0");
            var Gy2 = BigInteger.Parse("0");
            //var p = BigInteger.Parse("0FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFEFFFFFC2F", NumberStyles.HexNumber);
            //var b = (BigInteger)7;
            //var a = BigInteger.Zero;
            //var Gx = BigInteger.Parse("79BE667EF9DCBBAC55A06295CE870B07029BFCDB2DCE28D959F2815B16F81798", NumberStyles.HexNumber);
            //var Gy = BigInteger.Parse("483ada7726a3c4655da4fbfc0e1108a8fd17b448a68554199c47d08ffb10d4b8", NumberStyles.HexNumber);
            var p = BigInteger.Parse("115792089237316195423570985008687907853269984665640564039457584007908834671663");
            var b = (BigInteger)7;
            var a = BigInteger.Zero;
            //var Gx = BigInteger.Parse("43416641424355550452407462607989419545484660240025292632092983024276950562252"); //g * 1+10zer
            //var Gy = BigInteger.Parse("82258565099148626789977835510040746285752863850369846140886848753132330656544");
            //var order = BigInteger.Parse("115792089237316195423570985008687907852837564279074904382605163141518161494337");
            CurveFp curve256 = new CurveFp(p, a, b);
            Point generator256 = new Point(curve256, Gx, Gy);
            PointAdd punktAdd = new PointAdd();
            //Pointadd generator256add = new Pointadd(curve256, Gx, Gy);
            StreamWriter sw = new StreamWriter("D:\\Text.txt");

            var secret = BigInteger.Parse("1");
            var sum = BigInteger.Parse("0");
            var n = BigInteger.Parse("0");
            var last = BigInteger.Parse("0");
            var first = BigInteger.Parse("0");
            List<BigInteger> lista = new List<BigInteger>();
            BigInteger start = BigInteger.Parse("5");
            BigInteger dzielnik = BigInteger.Parse("25");
            BigInteger calosc = BigInteger.Parse("50");
            //BigInteger limit = BigInteger.Parse("10000000000");
            lista.Add(start);
            // dla 50
            var pubkeyPoint1 = generator256 * 50;
            Gx2 = pubkeyPoint1.X;
            Gy2 = pubkeyPoint1.Y;

            punktAdd.Addition(punktAdd, Gx1, Gy1, Gx2, Gy2, p, a);

            sum = 0;
            n = punktAdd.X3;
            last = n % 10;
            first = punktAdd.X3;
            while (first >= 10)
            {
                first /= 10;
            }
            while (n != 0)
            {
                sum += n % 10;
                n /= 10;
            }
            sw.WriteLine(first + " " + last + " " + sum.ToString("X"));

            while (true)
            {

                calosc = calosc + dzielnik;
                //Console.WriteLine(calosc);
                //sw.WriteLine("[" + calosc + "]");
                lista = tablica(lista, sw, dzielnik);
                dzielnik = dzielnik * 10;
                dzielnik = dzielnik / 2;
                calosc = calosc * 10;
                sw.WriteLine("[" + calosc + "]");
                Console.WriteLine(Convert.ToString(calosc).Length);
                //var pubkeyPoint = generator256 * secret;
                
                foreach (BigInteger prime in lista)
                {
                    var pubkeyPoint = generator256 * prime;
                    Gx2 = pubkeyPoint.X;
                    Gy2 = pubkeyPoint.Y;
                    
                    punktAdd.Addition(punktAdd, Gx1, Gy1, Gx2, Gy2, p, a);
                    
                    sum = 0;
                    n = punktAdd.X3;
                    last = n % 10;
                    first = punktAdd.X3;
                    while (first >= 10)
                    {
                        first /= 10;
                    }
                    while (n != 0)
                    {
                        sum += n % 10;
                        n /= 10;
                    }
                    sw.WriteLine(prime.ToString("X") + " " + first + " " + last + " " + sum);
                }
                //sum = 0;
                //n = pubkeyPoint.X;
                //last = n % 10;
                //first = pubkeyPoint.X;
                //while (first >= 10)
                //{
                //    first /= 10;
                //}
                //while (n != 0)
                //{
                //    sum += n % 10;
                //    n /= 10;
                //}
                
                //secret = secret + 1;

                //punktAdd.Addition(punktAdd, Gx1, Gy1, Gx2, Gy2, p, a);
                //var pubkeyPoint1 = generator256 * secret1;
                //sw.WriteLine("[" + secret + "] " + pubkeyPoint.X + " " + pubkeyPoint.Y);
                //if (pubkeyPoint.X < increment)
                //{
                //    Console.WriteLine(secret + "found");
                //    sw.WriteLine("[" + secret + "] " + pubkeyPoint.X + " " + pubkeyPoint.Y);
                //}
                //secret = secret + 1;
                //if ((secret % secret1) == 0)
                //{
                //    Console.WriteLine(secret);
                //}
                //secret1 = secret1 + 1;
                //licznik = licznik + 1;

                //Console.WriteLine("koniec");
                //Console.ReadLine();
            }
            sw.Flush();
        }
        public static List<BigInteger> tablica(List<BigInteger> tablista, StreamWriter sww, BigInteger dzielnik1)
        {
            List<BigInteger> tablica1 = new List<BigInteger>();
            //int wewnetrzny = 0;
            tablica1.Clear();
            foreach (BigInteger prime in tablista)
            {
                tablica1.Add(((prime * 10) - dzielnik1));
                tablica1.Add(((prime * 10) + dzielnik1));
                //sww.WriteLine(((prime * 10) + dzielnik1));
                //sww.WriteLine(((prime * 10) - dzielnik1));
            }
            //sww.Flush();
            return tablica1;
        }
        class Point
        {
            public static readonly Point INFINITY = new Point(null, default(BigInteger), default(BigInteger));
            public CurveFp Curve { get; private set; }
            public BigInteger X { get; private set; }
            public BigInteger Y { get; private set; }

            public Point(CurveFp curve, BigInteger x, BigInteger y)
            {
                this.Curve = curve;
                this.X = x;
                this.Y = y;
            }
            public Point Double()
            {
                if (this == INFINITY)
                    return INFINITY;

                BigInteger p = this.Curve.p;
                BigInteger a = this.Curve.a;
                BigInteger l = ((3 * this.X * this.X + a) * InverseMod(2 * this.Y, p)) % p;
                BigInteger x3 = (l * l - 2 * this.X) % p;
                BigInteger y3 = (l * (this.X - x3) - this.Y) % p;
                return new Point(this.Curve, x3, y3);
            }
            public override string ToString()
            {
                if (this == INFINITY)
                    return "infinity";
                return string.Format("({0},{1})", this.X, this.Y);
            }
            public static Point operator +(Point left, Point right)
            {
                if (right == INFINITY)
                    return left;
                if (left == INFINITY)
                    return right;
                if (left.X == right.X)
                {
                    if ((left.Y + right.Y) % left.Curve.p == 0)
                        return INFINITY;
                    else
                        return left.Double();
                }

                var p = left.Curve.p;
                var l = ((right.Y - left.Y) * InverseMod(right.X - left.X, p)) % p;
                var x3 = (l * l - left.X - right.X) % p;
                var y3 = (l * (left.X - x3) - left.Y) % p;
                return new Point(left.Curve, x3, y3);
            }
            public static Point operator *(Point left, BigInteger right)
            {
                var e = right;
                if (e == 0 || left == INFINITY)
                    return INFINITY;
                var e3 = 3 * e;
                var negativeLeft = new Point(left.Curve, left.X, -left.Y);
                var i = LeftmostBit(e3) / 2;
                var result = left;
                while (i > 1)
                {
                    result = result.Double();
                    if ((e3 & i) != 0 && (e & i) == 0)
                        result += left;
                    if ((e3 & i) == 0 && (e & i) != 0)
                        result += negativeLeft;
                    i /= 2;
                }
                return result;
            }

            private static BigInteger LeftmostBit(BigInteger x)
            {
                BigInteger result = 1;
                while (result <= x)
                    result = 2 * result;
                return result / 2;
            }
            private static BigInteger InverseMod(BigInteger a, BigInteger m)
            {
                while (a < 0) a += m;
                if (a < 0 || m <= a)
                    a = a % m;
                BigInteger c = a;
                BigInteger d = m;

                BigInteger uc = 1;
                BigInteger vc = 0;
                BigInteger ud = 0;
                BigInteger vd = 1;

                while (c != 0)
                {
                    BigInteger r;
                    //q, c, d = divmod( d, c ) + ( c, );
                    var q = BigInteger.DivRem(d, c, out r);
                    d = c;
                    c = r;

                    //uc, vc, ud, vd = ud - q*uc, vd - q*vc, uc, vc;
                    var uct = uc;
                    var vct = vc;
                    var udt = ud;
                    var vdt = vd;
                    uc = udt - q * uct;
                    vc = vdt - q * vct;
                    ud = uct;
                    vd = vct;
                }
                if (ud > 0) return ud;
                else return ud + m;
            }
        }
        public static BigInteger calcLambda1(BigInteger y2, BigInteger y1, BigInteger x2, BigInteger x1, BigInteger modspace)
        {

            BigInteger lam1 = (y2 - y1) * CalcInverse((x2 - x1), modspace);

            return lam1;
        }

        public static BigInteger calcLambda2(BigInteger x1, BigInteger y1, BigInteger A, BigInteger modspace)
        {

            BigInteger lam2 = (3 * x1 * x1 + A) * CalcInverse((2 * y1), modspace);
            return lam2;
        }


        public static BigInteger calcXFinal(BigInteger Lambda, BigInteger x1, BigInteger x2, BigInteger modspace)
        {
            BigInteger bigX = (Lambda * Lambda - x1 - x2) % modspace;
            if (bigX < 0) { bigX = bigX + modspace; }
            return bigX;

        }
        public static BigInteger calcYFinal(BigInteger Lambda, BigInteger x1, BigInteger x3, BigInteger y1, BigInteger modspace)
        {
            BigInteger bigY = (Lambda * (x1 - x3) - y1) % modspace;
            if (bigY < 0) { bigY = bigY + modspace; }
            return bigY;
        }
        public static BigInteger CalcInverse(BigInteger n, BigInteger p)
        {

            BigInteger x = 1;
            BigInteger y = 0;
            BigInteger a = p;
            BigInteger b = n;
            BigInteger q, t;
            BigInteger res;
            while (b != 0)
            {
                t = b;
                //q2 = Math.floor(a/t);
                q = BigInteger.Divide(a, t);
                b = a - q * t;
                a = t;
                t = x;
                x = y - q * t;
                y = t;
            }
            if (y < 0)
            {
                res = y + p;
            }
            else
            {
                res = y;
            }
            return res;

        }
        class PointAdd
        {
            /* public BigInteger X1 { get; private set; }
             public BigInteger Y1 { get; private set; }
             public BigInteger X2 { get; private set; }
             public BigInteger Y2 { get; private set; }*/
            public BigInteger X3 { get; private set; }
            public BigInteger Y3 { get; private set; }
            public BigInteger P { get; private set; }
            public BigInteger A { get; private set; }

            /*public PointAdd(BigInteger x1, BigInteger y1, BigInteger x2, BigInteger y2, BigInteger p, BigInteger a, BigInteger b)
            {
                this.X1 = x1;
                this.Y1 = y1;
                this.X2 = x2;
                this.Y2 = y2;
                this.P = p;
                this.A = a;
                this.B = b;
            }*/

            public PointAdd Addition(PointAdd punkt, BigInteger x1, BigInteger y1, BigInteger x2, BigInteger y2, BigInteger p, BigInteger a)
            {
                this.P = p;
                this.A = a;

                if ((x1 == x2) && (y1 != y2))
                {
                    this.X3 = 0;
                    this.Y3 = 0;
                    return punkt;
                }
                else
                {
                    if ((x1 == x2) && (y1 == y2))
                    {

                        BigInteger l2 = (calcLambda2(x1, y1, punkt.A, punkt.P)) % punkt.P;

                        BigInteger resl2x3 = (calcXFinal(l2, x1, x2, punkt.P)) % punkt.P;
                        BigInteger resl2y3 = (calcYFinal(l2, x1, resl2x3, y1, punkt.P)) % punkt.P;

                        this.X3 = resl2x3;
                        this.Y3 = resl2y3;

                        return punkt;
                    }
                    else
                    {

                        BigInteger l1 = (calcLambda1(y2, y1, x2, x1, punkt.P)) % punkt.P;

                        BigInteger resl1x3 = (calcXFinal(l1, x1, x2, punkt.P)) % punkt.P;
                        BigInteger resl1y3 = (calcYFinal(l1, x1, resl1x3, y1, punkt.P)) % punkt.P;

                        this.X3 = resl1x3;
                        this.Y3 = resl1y3;

                        return punkt;
                    }
                }
            }
        }
        class CurveFp
        {
            public BigInteger p { get; private set; }
            public BigInteger a { get; private set; }
            public BigInteger b { get; private set; }
            public CurveFp(BigInteger p, BigInteger a, BigInteger b)
            {
                this.p = p;
                this.a = a;
                this.b = b;
            }
        }
    }
}
