using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.IO;
using System.Runtime.Serialization;
using Newtonsoft.Json;
namespace ConsoleApplication7
{
    class Program
    {
        public  static string strFile=@"e:\logsser.log";
        static void Main(string[] args)
        {
            
            List<Shape> lstShapes = new List<Shape>();
            Circle circle= new Circle(20);
            Console.WriteLine($"Name {circle.name} Perimeter {circle.perimeter} Area {circle.area} ");

            Triangle trng = new Triangle(10, 10, 10, 7, 10);
            Console.WriteLine($"Name {trng.name} Perimeter {trng.perimeter} Area {trng.area} ");
            Triangle trngiso = new Triangle(20, 20, 10, 7, 10);
            Console.WriteLine($"Name {trngiso.name} Perimeter {trngiso.perimeter} Area {trngiso.area} ");

            Console.WriteLine(trng.name);
            Quadri q = new Quadri(10, 10, 10, 10);
            Console.WriteLine($"Name {q.name} Perimeter {q.perimeter} Area {q.area} ");
            for (int m=1;m<5;m++)
            {
                Quadri qv = new Quadri(m*2, m*2, 10, 10);
                Console.WriteLine($"Name {qv.name} Perimeter {qv.perimeter} Area {qv.area} ");
                lstShapes.Add(qv);
            }
            lstShapes.Add(circle);
            lstShapes.Add(trng);
            lstShapes.Add(q);
            List<Shape>  lstareaShapes = lstShapes.OrderBy(o => o.area).ToList();
            List<Shape>  lstPerimeterShapes = lstShapes.OrderBy(o => o.perimeter ).ToList();
            foreach (Shape crt in lstPerimeterShapes )
            {
                crt.SerializeObject(strFile);
            }
            Console.WriteLine("Total number of Circles:" + Program.Singleton.Instance.NoCircles);
            Console.WriteLine("Total number of Triangles:" + Program.Singleton.Instance.NoTriangles);
            Console.WriteLine("Total number of Quadri:" + Program.Singleton.Instance.NoQuadri);
            Console.ReadLine();
        }
        public sealed class Singleton
        {
            private Singleton()
            {
            }
            private static Singleton instance = null;
            public static Singleton Instance
            {
                get
                {
                    if (instance == null)
                    {
                        instance = new Singleton();
                    }
                    return instance;
                }
            }
            public int NoTriangles { get; set; }
            public int NoCircles { get; set; }
            public int NoQuadri { get; set; }

        }
    }

    public abstract class Shape 
        {
            public abstract string name { get; set; }
            public abstract float perimeter { get; }
            public abstract float area{get;}
           
            public void SerializeObject(string strFile)
            {
                var json = Newtonsoft.Json.JsonConvert.SerializeObject(this, Formatting.None, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });

               File.AppendAllText(strFile, json);
            }
            public Shape ()
            {
                Console.WriteLine ("Shape Constructor");
            }

        }


        public class Circle : Shape
        {

            private float mRadius;
            public float pi;
            public override string name { 
                get
                {
                    return "circle"+this.GetHashCode();
                }
                set 
                {
                }
            
            }
            public override float perimeter
            {
                get
                {
                    return 2 * pi * mRadius;
                }

            }
            public override float area
            {
                get
                {
                    return pi * mRadius * mRadius;
                }
            }
            public Circle(float radius) : base()
            {
                
                pi = 3.14F;
                mRadius = radius;
                Program.Singleton.Instance.NoCircles++; 
                Console.WriteLine("Circle Constructor");
                Console.WriteLine("Number of circles:" + Program.Singleton.Instance.NoCircles.ToString());
            }

        }


        public class Triangle : Shape
        {

            private float ma;
            public float mb;
            public float mc;
            public float mheight;
            public float mbase;
            public override string name { 
                get
                {
                    if ((ma == mb) && (mb == mc)) return "equilateral";
                    if ((ma == mb) || (mb == mc)) return "isosceles ";
                    if ((ma != mb) && (mb != mc) && (ma != mc)) return "scalene";
                    return "any";
                }
                set { 
                
                }
                
            }
            public override float perimeter
            {
                get
                {
                    return ma+mb+mc;
                }

            }
            public override float area
            {
                get
                {
                    return mheight *mbase/2;
                }
            }
            public Triangle(float a,float b, float c, float h, float _base):base()
            {
                ma = a;
                mb=b;
                mc = c;
                mheight = h;
                mbase = _base;
                Program.Singleton.Instance.NoTriangles++;
                Console.WriteLine("Triangle Constructor");
                Console.WriteLine("Number of triangles:" + Program.Singleton.Instance.NoTriangles.ToString());

            }

        }


    public class Quadri : Shape
    {

        private float ma;
        public float mb;
        public float mc;
        public float md;
        private float height;
        private float width;

        public override string name
        {
            get
            {
                if ((ma == mb) && (mb== mc) && (ma==md) && (mb==md )) return "square";
                if ( ((ma==mb) && (mc==md ) ) ||( (ma == md) && (mc ==mb) ) || ( (ma == mc) && (mb == md) ) ) return "Rectangle";
                return "any";
            }
            set
            {

            }

        }
        public override float perimeter
        {
            get
            {
                return ma + mb + mc + md;
            }

        }
        public override float area
        {
            get
            {
                return (height *width );
            }
        }
        public Quadri(float a, float b, float c, float d) : base()
        {
            ma = a;
            mb = b;
            mc = c;
            md = d;
            List<float> fltEdges = new List<float>();
            fltEdges.Add(ma);fltEdges.Add(mb);fltEdges.Add(mc);fltEdges.Add(md);
            fltEdges.Sort();
            bool blnOk = false;//not good rectangle
            if (((ma == mb) && (mc == md)) || ((ma == md) && (mc == mb)) || ((ma == mc) && (mb == md)))
            {
                height = fltEdges[0];
                width = fltEdges[1];

            }
            else
            {
                height = -1;
                width = -1;
            }
            Program.Singleton.Instance.NoQuadri++;
            Console.WriteLine("Quadri Constructor");
            Console.WriteLine("Number of Quadri:" + Program.Singleton.Instance.NoTriangles.ToString());

        }

    }



}

