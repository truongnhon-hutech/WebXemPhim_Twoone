using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace MovieWeb.Models
{
    public class Line
    {
        
        public string type = "line";
        public Data data;
        public Options options = new Options();


        public class Options
        {
            public Plugins plugins = new Plugins();
            public class Plugins
            {
                public Title title = new Title();
                public class Title
                {
                    public bool display = true;
                    public string text = "hi";
                }
            }
        }
        public class Data
        {
            public string[] labels;
            public Dataset[] datasets;
            public class Dataset
            {
                public string label;
                public float[] data;
                public bool fill = false;
                public string borderColor ;
                public float tension = 0.45f;
                private static readonly Random rand = new Random();

                private string GetRandomColour()
                {
                    int x = (DateTime.Now.Millisecond + DateTime.Now.Second)%256;
                    int y = (DateTime.Now.Millisecond + DateTime.Now.Second + DateTime.Now.Hour) % 256;
                    int z = (DateTime.Now.Millisecond + DateTime.Now.Second + DateTime.Now.Minute) % 256;
                    return Color.FromArgb(x,y,z,(x+y+z)%256).ToString();
                }

            }
            public Data(int sogois)
            {
                datasets = new Dataset[sogois];
                for(int i=0;i<sogois;i++)
                    datasets[i] = new Dataset();
                
            }
        }
        public Line(int sogois)
        {
            data = new Data(sogois);
        }

    }
}