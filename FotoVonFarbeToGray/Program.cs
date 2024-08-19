using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

public static class Program
{

    public static float NextFloat(this Random random, float minValue, float maxValue)
    {
        return (float)(random.NextDouble() * (maxValue - minValue) + minValue);
    }

    static List<string> GetFileFormats(string path)
    {
        List<string> formats = new List<string>();

        try
        {
            // Hole alle Dateien im angegebenen Verzeichnis
            string[] files = Directory.GetFiles(path);

            foreach (string file in files)
            {
                // Hole die Dateierweiterung
                string extension = Path.GetExtension(file).ToLower();

                // Wenn die Erweiterung noch nicht in der Liste ist, füge sie hinzu
                if (!formats.Contains(extension))
                {
                    formats.Add(extension);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fehler: {ex.Message}");
        }

        return formats;
    }

    public static List<string>? GetAllOrdner(string path)
    {
        List<string> _subdirectories = new List<string>();
        if (Directory.Exists(path))
        {
            string[] _subDirectories = Directory.GetDirectories(path, "*", SearchOption.AllDirectories);

            if (_subDirectories.Length != 0)
            {
                foreach (var item in _subDirectories)
                {
                    _subdirectories.Add(item);

                }
                return _subdirectories;

            }
            else
            {
                //Console.WriteLine("Da existiert kein Subdirectories");
                return null;
            }
        }
        else
        {
            //Console.WriteLine("Directory existiert nicht!");
            return null;
        }
    }

    public static List<string>? GetAllFiles(string path)
    {
        List<string> _files = new List<string>();
        if (Directory.Exists(path))
        {
            string[] files = Directory.GetFiles(path, "*", SearchOption.TopDirectoryOnly);

            if (files.Length != 0)
            {
                foreach (var item in files)
                {
                    _files.Add(item);
                }
                return _files;
            }
            else
            {
                //Console.WriteLine("Da gibt es keine Datei!");
                return null;
            }

        }
        else
        {
            //Console.WriteLine("Directory existiert nicht");
            return null;
        }
    }

    #region Foto Bearbeitung

    public static void FotoResize(List<string> dataPath, string directoryPath)
    {
        try
        {
            foreach (var item in dataPath)
            {
                string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(item);
                //string outputFileName = $"{fileNameWithoutExtension}_R.png";


                using (Image<Rgba32> image = Image.Load<Rgba32>(item))
                {

                    if (image.Height != 0 && image.Width != 0)
                    {
                        double scaleFactor = 0.25;

                        for (int i = 0; i < 3; i++)
                        {

                            int newHeight = (int)(image.Height * scaleFactor);
                            int newWidth = (int)(image.Width * scaleFactor);

                            using (var resizedImage = image.Clone(x => x.Resize(newWidth, newHeight)))
                            {
                                string outputFileName = $"{fileNameWithoutExtension}_R{scaleFactor}.png";
                                string outputFilePath = Path.Combine(directoryPath, outputFileName);

                                resizedImage.Save(outputFilePath);
                            }

                            scaleFactor += 0.25;
                        }
                    }
                    else
                    {
                        continue;
                    }
                }

            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fehler während der Resize {ex.Message}");
        }
    }

    public static void FotoRotation(List<string> dataPath, string directoryPath)
    {
        try
        {
            foreach (var item in dataPath)
            {
                string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(item);
                //string outputFileName = $"{fileNameWithoutExtension}_R.png";


                using (Image<Rgba32> image = Image.Load<Rgba32>(item))
                {

                    if (image.Height != 0 && image.Width != 0)
                    {
                        int rotateFactor = 90;

                        for (int i = 0; i < 3; i++)
                        {



                            using (var resizedImage = image.Clone(x => x.Rotate(rotateFactor)))
                            {
                                string outputFileName = $"{fileNameWithoutExtension}_RT{rotateFactor}.png";
                                string outputFilePath = Path.Combine(directoryPath, outputFileName);

                                resizedImage.Save(outputFilePath);
                            }

                            rotateFactor += 90;
                        }
                    }
                    else
                    {
                        continue;
                    }
                }

            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fehler während der Resize {ex.Message}");
        }
    }

    public static void FotoFlipHorisontal(List<string> dataPath, string directoryPath)
    {
        try
        {
            foreach (var item in dataPath)
            {
                string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(item);
                //string outputFileName = $"{fileNameWithoutExtension}_R.png";


                using (Image<Rgba32> image = Image.Load<Rgba32>(item))
                {

                    if (image.Height != 0 && image.Width != 0)
                    {

                        using (var resizedImage = image.Clone(x => x.Flip(FlipMode.Horizontal)))
                        {
                            string outputFileName = $"{fileNameWithoutExtension}_FLH.png";
                            string outputFilePath = Path.Combine(directoryPath, outputFileName);

                            resizedImage.Save(outputFilePath);
                        }

                    }
                    else
                    {
                        continue;
                    }
                }

            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fehler während der Resize {ex.Message}");
        }
    }

    public static void FotoFlipVertical(List<string> dataPath, string directoryPath)
    {
        try
        {
            foreach (var item in dataPath)
            {
                string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(item);
                //string outputFileName = $"{fileNameWithoutExtension}_R.png";


                using (Image<Rgba32> image = Image.Load<Rgba32>(item))
                {

                    if (image.Height != 0 && image.Width != 0)
                    {

                        using (var resizedImage = image.Clone(x => x.Flip(FlipMode.Vertical)))
                        {
                            string outputFileName = $"{fileNameWithoutExtension}_FLV.png";
                            string outputFilePath = Path.Combine(directoryPath, outputFileName);

                            resizedImage.Save(outputFilePath);
                        }

                    }
                    else
                    {
                        continue;
                    }
                }

            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fehler während der Resize {ex.Message}");
        }
    }

    public static void FotoFarbeGray(List<string> dataPath, string directoryPath)
    {
        try
        {
            foreach (var item in dataPath)
            {
                string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(item);
                //string outputFileName = $"{fileNameWithoutExtension}_R.png";


                using (Image<Rgba32> image = Image.Load<Rgba32>(item))
                {

                    if (image.Height != 0 && image.Width != 0)
                    {


                        using (var resizedImage = image.Clone(x => x.Grayscale()))
                        {
                            string outputFileName = $"{fileNameWithoutExtension}_FARG.png";
                            string outputFilePath = Path.Combine(directoryPath, outputFileName);

                            resizedImage.Save(outputFilePath);
                        }

                    }
                    else
                    {
                        continue;
                    }
                }

            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fehler während der Resize {ex.Message}");
        }
    }

    public static void FotoBrightnessDunkel(List<string> dataPath, string directoryPath)
    {
        try
        {
            foreach (var item in dataPath)
            {
                string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(item);
                //string outputFileName = $"{fileNameWithoutExtension}_R.png";


                using (Image<Rgba32> image = Image.Load<Rgba32>(item))
                {

                    if (image.Height != 0 && image.Width != 0)
                    {
                        var _brightness = 1.0f - 0.25f;
                        for (int i = 0; i < 3; i++)
                        {
                            using (var resizedImage = image.Clone(x => x.Brightness(_brightness)))
                            {
                                string outputFileName = $"{fileNameWithoutExtension}_BR_dunk{_brightness}.png";
                                string outputFilePath = Path.Combine(directoryPath, outputFileName);

                                resizedImage.Save(outputFilePath);
                            }

                            _brightness -= 0.25f;
                        }
                    }
                    else
                    {
                        continue;
                    }
                }

            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fehler während der Resize {ex.Message}");
        }
    }

    public static void FotoBrightnessHell(List<string> dataPath, string directoryPath)
    {
        try
        {
            foreach (var item in dataPath)
            {
                string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(item);
                //string outputFileName = $"{fileNameWithoutExtension}_R.png";


                using (Image<Rgba32> image = Image.Load<Rgba32>(item))
                {

                    if (image.Height != 0 && image.Width != 0)
                    {
                        var _brightness = 1.0f + 0.25f;
                        for (int i = 0; i < 3; i++)
                        {
                            using (var resizedImage = image.Clone(x => x.Brightness(_brightness)))
                            {
                                string outputFileName = $"{fileNameWithoutExtension}_BR_dunk{_brightness}.png";
                                string outputFilePath = Path.Combine(directoryPath, outputFileName);

                                resizedImage.Save(outputFilePath);
                            }

                            _brightness += 0.25f;
                        }
                    }
                    else
                    {
                        continue;
                    }
                }

            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fehler während der Resize {ex.Message}");
        }
    }

    public static void FotoContrastUnten(List<string> dataPath, string directoryPath)
    {
        try
        {
            foreach (var item in dataPath)
            {
                string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(item);
                //string outputFileName = $"{fileNameWithoutExtension}_R.png";


                using (Image<Rgba32> image = Image.Load<Rgba32>(item))
                {

                    if (image.Height != 0 && image.Width != 0)
                    {
                        var Contrast = 1.0f - 0.25f;
                        for (int i = 0; i < 3; i++)
                        {
                            using (var resizedImage = image.Clone(x => x.Contrast(Contrast)))
                            {
                                string outputFileName = $"{fileNameWithoutExtension}_CON_unt{Contrast}.png";
                                string outputFilePath = Path.Combine(directoryPath, outputFileName);

                                resizedImage.Save(outputFilePath);
                            }

                            Contrast -= 0.25f;
                        }
                    }
                    else
                    {
                        continue;
                    }
                }

            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fehler während der Resize {ex.Message}");
        }
    }

    public static void FotoContrastOben(List<string> dataPath, string directoryPath)
    {
        try
        {
            foreach (var item in dataPath)
            {
                string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(item);
                //string outputFileName = $"{fileNameWithoutExtension}_R.png";


                using (Image<Rgba32> image = Image.Load<Rgba32>(item))
                {

                    if (image.Height != 0 && image.Width != 0)
                    {
                        var Contrast = 1.0f + 0.25f;
                        for (int i = 0; i < 3; i++)
                        {
                            using (var resizedImage = image.Clone(x => x.Contrast(Contrast)))
                            {
                                string outputFileName = $"{fileNameWithoutExtension}_CON_OBN{Contrast}.png";
                                string outputFilePath = Path.Combine(directoryPath, outputFileName);

                                resizedImage.Save(outputFilePath);
                            }

                            Contrast += 0.25f;
                        }
                    }
                    else
                    {
                        continue;
                    }
                }

            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fehler während der Resize {ex.Message}");
        }
    }

    public static void FotoSaturateUnten(List<string> dataPath, string directoryPath)
    {
        try
        {
            foreach (var item in dataPath)
            {
                string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(item);
                //string outputFileName = $"{fileNameWithoutExtension}_R.png";


                using (Image<Rgba32> image = Image.Load<Rgba32>(item))
                {

                    if (image.Height != 0 && image.Width != 0)
                    {
                        var Saturate = 1.0f - 0.25f;
                        for (int i = 0; i < 3; i++)
                        {
                            using (var resizedImage = image.Clone(x => x.Contrast(Saturate)))
                            {
                                string outputFileName = $"{fileNameWithoutExtension}_SAT_UNT{Saturate}.png";
                                string outputFilePath = Path.Combine(directoryPath, outputFileName);

                                resizedImage.Save(outputFilePath);
                            }

                            Saturate -= 0.25f;
                        }
                    }
                    else
                    {
                        continue;
                    }
                }

            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fehler während der Resize {ex.Message}");
        }
    }

    public static void FotoSaturateOben(List<string> dataPath, string directoryPath)
    {
        try
        {
            foreach (var item in dataPath)
            {
                string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(item);
                //string outputFileName = $"{fileNameWithoutExtension}_R.png";


                using (Image<Rgba32> image = Image.Load<Rgba32>(item))
                {

                    if (image.Height != 0 && image.Width != 0)
                    {
                        var Saturate = 1.0f + 0.25f;
                        for (int i = 0; i < 3; i++)
                        {
                            using (var resizedImage = image.Clone(x => x.Contrast(Saturate)))
                            {
                                string outputFileName = $"{fileNameWithoutExtension}_SAT_OBN{Saturate}.png";
                                string outputFilePath = Path.Combine(directoryPath, outputFileName);

                                resizedImage.Save(outputFilePath);
                            }

                            Saturate += 0.25f;
                        }
                    }
                    else
                    {
                        continue;
                    }
                }

            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fehler während der Resize {ex.Message}");
        }
    }

    public static void FotoGaussianBlur(List<string> dataPath, string directoryPath)
    {
        try
        {
            foreach (var item in dataPath)
            {
                string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(item);
                //string outputFileName = $"{fileNameWithoutExtension}_R.png";


                using (Image<Rgba32> image = Image.Load<Rgba32>(item))
                {

                    if (image.Height != 0 && image.Width != 0)
                    {


                        using (var resizedImage = image.Clone(x => x.GaussianBlur()))
                        {
                            string outputFileName = $"{fileNameWithoutExtension}_GAUB.png";
                            string outputFilePath = Path.Combine(directoryPath, outputFileName);

                            resizedImage.Save(outputFilePath);
                        }

                    }
                    else
                    {
                        continue;
                    }
                }

            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fehler während der Resize {ex.Message}");
        }
    }

    public static void FotoGaussianSharpen(List<string> dataPath, string directoryPath)
    {
        try
        {
            foreach (var item in dataPath)
            {
                string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(item);
                //string outputFileName = $"{fileNameWithoutExtension}_R.png";


                using (Image<Rgba32> image = Image.Load<Rgba32>(item))
                {

                    if (image.Height != 0 && image.Width != 0)
                    {


                        using (var resizedImage = image.Clone(x => x.GaussianSharpen()))
                        {
                            string outputFileName = $"{fileNameWithoutExtension}_GAUS.png";
                            string outputFilePath = Path.Combine(directoryPath, outputFileName);

                            resizedImage.Save(outputFilePath);
                        }

                    }
                    else
                    {
                        continue;
                    }
                }

            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fehler während der Resize {ex.Message}");
        }
    }

    //10 mal skew
    public static void FotoSkewPlus(List<string> dataPath, string directoryPath)
    {
        try
        {
            foreach (var item in dataPath)
            {
                string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(item);
                //string outputFileName = $"{fileNameWithoutExtension}_R.png";


                using (Image<Rgba32> image = Image.Load<Rgba32>(item))
                {

                    if (image.Height != 0 && image.Width != 0)
                    {
                        Random rnd = new Random();

                        for (int i = 0; i < 9; i++)
                        {
                            float skewAmount = (float)(rnd.NextDouble() * 40 - 20);
                            using (var resizedImage = image.Clone(x => x.Skew(skewAmount, skewAmount)))
                            {
                                string outputFileName = $"{fileNameWithoutExtension}_skew_plus{skewAmount}.png";
                                string outputFilePath = Path.Combine(directoryPath, outputFileName);

                                resizedImage.Save(outputFilePath);
                            }


                        }
                    }
                    else
                    {
                        continue;
                    }
                }

            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fehler während der Resize {ex.Message}");
        }
    }
    //10 mal skew
    public static void FotoSkewMinus(List<string> dataPath, string directoryPath)
    {
        try
        {
            foreach (var item in dataPath)
            {
                string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(item);
                //string outputFileName = $"{fileNameWithoutExtension}_R.png";


                using (Image<Rgba32> image = Image.Load<Rgba32>(item))
                {

                    if (image.Height != 0 && image.Width != 0)
                    {
                        Random rnd = new Random();

                        for (int i = 0; i < 9; i++)
                        {
                            float skewAmount = (float)(rnd.NextDouble() * 40 - 20);
                            using (var resizedImage = image.Clone(x => x.Skew(skewAmount, skewAmount)))
                            {
                                string outputFileName = $"{fileNameWithoutExtension}_skew_minus{skewAmount}.png";
                                string outputFilePath = Path.Combine(directoryPath, outputFileName);

                                resizedImage.Save(outputFilePath);
                            }

                        }
                    }
                    else
                    {
                        continue;
                    }
                }

            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fehler während der Resize {ex.Message}");
        }
    }

    public static void FotoAdaptiveThreshold(List<string> dataPath, string directoryPath)
    {
        try
        {
            foreach (var item in dataPath)
            {
                string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(item);
                //string outputFileName = $"{fileNameWithoutExtension}_R.png";


                using (Image<Rgba32> image = Image.Load<Rgba32>(item))
                {

                    if (image.Height != 0 && image.Width != 0)
                    {
                        using (var resizedImage = image.Clone(x => x.AdaptiveThreshold()))
                        {
                            string outputFileName = $"{fileNameWithoutExtension}_ADP.png";
                            string outputFilePath = Path.Combine(directoryPath, outputFileName);

                            resizedImage.Save(outputFilePath);
                        }

                    }
                    else
                    {
                        continue;
                    }
                }

            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fehler während der Resize {ex.Message}");
        }
    }

    public static void FotoQuantizeWebSafe(List<string> dataPath, string directoryPath)
    {
        try
        {
            foreach (var item in dataPath)
            {
                string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(item);
                //string outputFileName = $"{fileNameWithoutExtension}_R.png";


                using (Image<Rgba32> image = Image.Load<Rgba32>(item))
                {

                    if (image.Height != 0 && image.Width != 0)
                    {
                        using (var resizedImage = image.Clone(x => x.Quantize(KnownQuantizers.WebSafe)))
                        {
                            string outputFileName = $"{fileNameWithoutExtension}_WEBSAFE.png";
                            string outputFilePath = Path.Combine(directoryPath, outputFileName);

                            resizedImage.Save(outputFilePath);
                        }

                    }
                    else
                    {
                        continue;
                    }
                }

            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fehler während der Resize {ex.Message}");
        }
    }

    public static void FotoQuantizeOctree(List<string> dataPath, string directoryPath)
    {
        try
        {
            foreach (var item in dataPath)
            {
                string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(item);
                //string outputFileName = $"{fileNameWithoutExtension}_R.png";


                using (Image<Rgba32> image = Image.Load<Rgba32>(item))
                {

                    if (image.Height != 0 && image.Width != 0)
                    {
                        using (var resizedImage = image.Clone(x => x.Quantize(KnownQuantizers.Octree)))
                        {
                            string outputFileName = $"{fileNameWithoutExtension}_Octree.png";
                            string outputFilePath = Path.Combine(directoryPath, outputFileName);

                            resizedImage.Save(outputFilePath);
                        }

                    }
                    else
                    {
                        continue;
                    }
                }

            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fehler während der Resize {ex.Message}");
        }
    }

    public static void FotoQuantizeWu(List<string> dataPath, string directoryPath)
    {
        try
        {
            foreach (var item in dataPath)
            {
                string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(item);
                //string outputFileName = $"{fileNameWithoutExtension}_R.png";


                using (Image<Rgba32> image = Image.Load<Rgba32>(item))
                {

                    if (image.Height != 0 && image.Width != 0)
                    {
                        using (var resizedImage = image.Clone(x => x.Quantize(KnownQuantizers.Wu)))
                        {
                            string outputFileName = $"{fileNameWithoutExtension}_Wu.png";
                            string outputFilePath = Path.Combine(directoryPath, outputFileName);

                            resizedImage.Save(outputFilePath);
                        }

                    }
                    else
                    {
                        continue;
                    }
                }

            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fehler während der Resize {ex.Message}");
        }
    }

    public static void FotoQuantizeWerner(List<string> dataPath, string directoryPath)
    {
        try
        {
            foreach (var item in dataPath)
            {
                string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(item);
                //string outputFileName = $"{fileNameWithoutExtension}_R.png";


                using (Image<Rgba32> image = Image.Load<Rgba32>(item))
                {

                    if (image.Height != 0 && image.Width != 0)
                    {
                        using (var resizedImage = image.Clone(x => x.Quantize(KnownQuantizers.Werner)))
                        {
                            string outputFileName = $"{fileNameWithoutExtension}_Werner.png";
                            string outputFilePath = Path.Combine(directoryPath, outputFileName);

                            resizedImage.Save(outputFilePath);
                        }

                    }
                    else
                    {
                        continue;
                    }
                }

            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fehler während der Resize {ex.Message}");
        }
    }

    #endregion

    public static void FotoBearbeitung(string path)

    {
        try
        {
            List<string>? _directory = new();
            List<string>? _files = new();

            _directory = GetAllOrdner(path);
            _files = GetAllFiles(path);

            if (_files != null && _directory == null)
            {
                //aufrufen Methode

                FotoResize(_files, path);
                FotoRotation(_files, path);
                FotoFlipHorisontal(_files, path);
                FotoFlipVertical(_files, path);
                FotoFarbeGray(_files, path);
                FotoBrightnessDunkel(_files, path);
                FotoBrightnessHell(_files, path);
                FotoContrastOben(_files, path);
                FotoContrastUnten(_files, path);
                FotoSaturateOben(_files, path);
                FotoSaturateUnten(_files, path);
                FotoGaussianBlur(_files, path);
                FotoGaussianSharpen(_files, path);
                FotoSkewPlus(_files, path);
                FotoSkewMinus(_files, path);
                FotoAdaptiveThreshold(_files, path);
                FotoQuantizeWebSafe(_files, path);
                FotoQuantizeOctree(_files, path);
                FotoQuantizeWu(_files, path);
                FotoQuantizeWerner(_files, path);

            }
            else
            {
                Parallel.ForEach(_directory, item =>
                {
                    List<string> _dirFiles = new();
                    _dirFiles = GetAllFiles(item);

                    FotoResize(_dirFiles, item);
                    FotoRotation(_dirFiles, item);
                    FotoFlipHorisontal(_dirFiles, item);
                    FotoFlipVertical(_dirFiles, item);
                    FotoFarbeGray(_dirFiles, item);
                    FotoBrightnessDunkel(_dirFiles, item);
                    FotoBrightnessHell(_dirFiles, item);
                    FotoContrastOben(_files, path);
                    FotoContrastUnten(_files, path);
                    FotoSaturateOben(_files, path);
                    FotoSaturateUnten(_files, path);
                    FotoGaussianBlur(_files, path);
                    FotoGaussianSharpen(_files, path);
                    FotoSkewPlus(_files, path);
                    FotoSkewMinus(_files, path);
                    FotoAdaptiveThreshold(_files, path);
                    FotoQuantizeWebSafe(_files, path);
                    FotoQuantizeOctree(_files, path);
                    FotoQuantizeWu(_files, path);
                    FotoQuantizeWerner(_files, path);
                });


            }

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }


    }





    private static void Main(string[] args)
    {
        bool arbeit = true;
        while (arbeit)
        {
            Console.WriteLine("Achtung! 1 Bild = +52");
            Console.WriteLine("1. start");

            int option = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("path: ");

            string path = Console.ReadLine();

            switch (option)
            {
                case 1:
                    FotoBearbeitung(path);
                    break;
                default:
                    break;
            }
        }


    }
}

