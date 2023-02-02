using WkHtmlToPdfDotNet;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;


namespace WKhtmltopdfSample
{
    internal static class Program
    {
        static  BasicConverter bConverter ;
        static  STASynchronizedConverter converter;

        public static void Main(string[] args)
        {
            Setup();
         // var summary = BenchmarkRunner.Run(typeof(Program).Assembly);
            
        }


        public static void Setup()
        {
            var doc = new HtmlToPdfDocument()
            {
                GlobalSettings = {
                    ColorMode = ColorMode.Grayscale,
                    Orientation = Orientation.Portrait,
                    PaperSize = PaperKind.A4,
                    Margins = new MarginSettings() { Top = 10 },
                },
                Objects = {
                    new ObjectSettings() {
                          PagesCount = true,
    HtmlContent = TemplateGenerator.GetVunerableHTMLString(),
    WebSettings = { DefaultEncoding = "utf-8", UserStyleSheet =   Path.Combine(Directory.GetCurrentDirectory(), "assets",  "styles.css") },
    HeaderSettings = { FontName = "Arial", FontSize = 9, Right = "Page [page] of [toPage]", Line = true },
    FooterSettings = { FontName = "Arial", FontSize = 9, Line = true, Center = "Report Footer" }
                    }
                }
            };

            Task.Run(() => Action(doc));

            var doc2 = new HtmlToPdfDocument()
            {
                GlobalSettings = {
                    PaperSize = PaperKind.A4Small
                },

                Objects = {
                    new ObjectSettings()
                    {
                        Page = "https://softdevplus.com/",
                    }
                }
            };


            Task.Run(() => Action(doc2));

            Task.Run(() => Basic(doc));

            Console.ReadKey();
        }

        [Benchmark(Description = "MultiThreading using BlockingCollections")]
        private static void Action(HtmlToPdfDocument doc)
        {
            byte[] pdf = converter.Convert(doc);

            if (!Directory.Exists("Files"))
            {
                Directory.CreateDirectory("Files");
            }

            using (FileStream stream = new FileStream(@"Files\" + DateTime.UtcNow.Ticks.ToString() + ".pdf", FileMode.Create))
            {
                stream.Write(pdf, 0, pdf.Length);
            }

        }


        [Benchmark(Description = "MultiThreading using Basic Collection")]
        private static void Basic(HtmlToPdfDocument doc)
        {
            byte[] pdf = bConverter.Convert(doc);

            if (!Directory.Exists("Files"))
            {
                Directory.CreateDirectory("Files");
            }

            using (FileStream stream = new FileStream(@"Files\" + DateTime.UtcNow.Ticks.ToString() + ".pdf", FileMode.Create))
            {
                stream.Write(pdf, 0, pdf.Length);
            }

        }
    }
}