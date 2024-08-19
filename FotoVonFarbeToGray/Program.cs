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
            int totalImages = dataPath.Count;
            int currentImage = 0;

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

                    currentImage++;
                    double progress = (double)currentImage / totalImages * 100;
                    Console.Write($"\rResize: [{new string('#', currentImage)}{new string(' ', totalImages - currentImage)}] {progress:F2}%");
                }

            }
            Console.WriteLine("\nResize abgeschlossen!");
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
            int totalImages = dataPath.Count;
            int currentImage = 0;

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
                    currentImage++;
                    double progress = (double)currentImage / totalImages * 100;
                    Console.Write($"\rRotation: [{new string('#', currentImage)}{new string(' ', totalImages - currentImage)}] {progress:F2}%");
                }

            }
            Console.WriteLine("\nRotation abgeschlossen!");
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
            int totalImages = dataPath.Count;
            int currentImage = 0;
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
                    currentImage++;
                    double progress = (double)currentImage / totalImages * 100;
                    Console.Write($"\rFlip Horisontal: [{new string('#', currentImage)}{new string(' ', totalImages - currentImage)}] {progress:F2}%");
                }

            }
            Console.WriteLine("\nFlip Horisontal abgeschlossen!");
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
            int totalImages = dataPath.Count;
            int currentImage = 0;
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
                    currentImage++;
                    double progress = (double)currentImage / totalImages * 100;
                    Console.Write($"\rFlip Vertical: [{new string('#', currentImage)}{new string(' ', totalImages - currentImage)}] {progress:F2}%");
                }

            }
            Console.WriteLine("\nFlip Vertical abgeschlossen!");
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
            int totalImages = dataPath.Count;
            int currentImage = 0;
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
                    // Aktualisiere die Fortschrittsanzeige
                    currentImage++;
                    double progress = (double)currentImage / totalImages * 100;
                    Console.Write($"\rFarbe Gray: [{new string('#', currentImage)}{new string(' ', totalImages - currentImage)}] {progress:F2}%");
                }
                Console.WriteLine("\nFarbe Gray abgeschlossen!");

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
            int totalImages = dataPath.Count;
            int currentImage = 0;
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
                    currentImage++;
                    double progress = (double)currentImage / totalImages * 100;
                    Console.Write($"\rBrightness Dunkel: [{new string('#', currentImage)}{new string(' ', totalImages - currentImage)}] {progress:F2}%");
                }

            }
            Console.WriteLine("\nBrightness Dunkel abgeschlossen!");
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
            int totalImages = dataPath.Count;
            int currentImage = 0;
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
                    // Aktualisiere die Fortschrittsanzeige
                    currentImage++;
                    double progress = (double)currentImage / totalImages * 100;
                    Console.Write($"\rBrightness Hell: [{new string('#', currentImage)}{new string(' ', totalImages - currentImage)}] {progress:F2}%");
                }

            }
            Console.WriteLine("\nBrightness Hell abgeschlossen!");
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
            int totalImages = dataPath.Count;
            int currentImage = 0;
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
                    currentImage++;
                    double progress = (double)currentImage / totalImages * 100;
                    Console.Write($"\rContrast niedrig: [{new string('#', currentImage)}{new string(' ', totalImages - currentImage)}] {progress:F2}%");
                }

            }
            Console.WriteLine("\nContrast niedrig abgeschlossen!");
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
            int totalImages = dataPath.Count;
            int currentImage = 0;
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
                    currentImage++;
                    double progress = (double)currentImage / totalImages * 100;
                    Console.Write($"\rContrast hoch: [{new string('#', currentImage)}{new string(' ', totalImages - currentImage)}] {progress:F2}%");
                }

            }
            Console.WriteLine("\nContrast hoch abgeschlossen!");
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
            int totalImages = dataPath.Count;
            int currentImage = 0;
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
                    currentImage++;
                    double progress = (double)currentImage / totalImages * 100;
                    Console.Write($"\rSaturate niedrig: [{new string('#', currentImage)}{new string(' ', totalImages - currentImage)}] {progress:F2}%");
                }

            }
            Console.WriteLine("\nSaturate niedrig abgeschlossen!");
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
            int totalImages = dataPath.Count;
            int currentImage = 0;
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
                    currentImage++;
                    double progress = (double)currentImage / totalImages * 100;
                    Console.Write($"\rSaturate hoch: [{new string('#', currentImage)}{new string(' ', totalImages - currentImage)}] {progress:F2}%");
                }

            }
            Console.WriteLine("\nSaturate hoch abgeschlossen!");
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
            int totalImages = dataPath.Count;
            int currentImage = 0;
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
                    currentImage++;
                    double progress = (double)currentImage / totalImages * 100;
                    Console.Write($"\rGaussian Blur: [{new string('#', currentImage)}{new string(' ', totalImages - currentImage)}] {progress:F2}%");
                }

            }
            Console.WriteLine("\nGaussian Blur abgeschlossen!");
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
            int totalImages = dataPath.Count;
            int currentImage = 0;
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
                    currentImage++;
                    double progress = (double)currentImage / totalImages * 100;
                    Console.Write($"\rGaussian Sharpen: [{new string('#', currentImage)}{new string(' ', totalImages - currentImage)}] {progress:F2}%");
                }

            }
            Console.WriteLine("\nGaussian Sharpen abgeschlossen!");
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
            int totalImages = dataPath.Count;
            int currentImage = 0;
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
                        Console.WriteLine("\nSkew Plus abgeschlossen!");
                    }
                    else
                    {
                        continue;
                    }
                    currentImage++;
                    double progress = (double)currentImage / totalImages * 100;
                    Console.Write($"\rSkew Plus: [{new string('#', currentImage)}{new string(' ', totalImages - currentImage)}] {progress:F2}%");
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
            int totalImages = dataPath.Count;
            int currentImage = 0;
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
                    currentImage++;
                    double progress = (double)currentImage / totalImages * 100;
                    Console.Write($"\rSkew Minus: [{new string('#', currentImage)}{new string(' ', totalImages - currentImage)}] {progress:F2}%");
                }

            }
            Console.WriteLine("\nSkew Minus abgeschlossen!");
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
            int totalImages = dataPath.Count;
            int currentImage = 0;
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
                    currentImage++;
                    double progress = (double)currentImage / totalImages * 100;
                    Console.Write($"\rAdaptive Threshold: [{new string('#', currentImage)}{new string(' ', totalImages - currentImage)}] {progress:F2}%");
                }

            }
            Console.WriteLine("\nAdaptive Threshold abgeschlossen!");
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
            int totalImages = dataPath.Count;
            int currentImage = 0;
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
                    currentImage++;
                    double progress = (double)currentImage / totalImages * 100;
                    Console.Write($"\rQuantize WebSafe: [{new string('#', currentImage)}{new string(' ', totalImages - currentImage)}] {progress:F2}%");
                }

            }
            Console.WriteLine("\nQuantize Web Safe abgeschlossen!");
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
            int totalImages = dataPath.Count;
            int currentImage = 0;
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
                    currentImage++;
                    double progress = (double)currentImage / totalImages * 100;
                    Console.Write($"\rQuantize Octree: [{new string('#', currentImage)}{new string(' ', totalImages - currentImage)}] {progress:F2}%");
                }

            }
            Console.WriteLine("\nQuantize Octree abgeschlossen!");
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
            int totalImages = dataPath.Count;
            int currentImage = 0;
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
                    currentImage++;
                    double progress = (double)currentImage / totalImages * 100;
                    Console.Write($"\rQuantize Wu: [{new string('#', currentImage)}{new string(' ', totalImages - currentImage)}] {progress:F2}%");
                }

            }
            Console.WriteLine("\nQuantize Wu abgeschlossen!");
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
            int totalImages = dataPath.Count;
            int currentImage = 0;
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
                    currentImage++;
                    double progress = (double)currentImage / totalImages * 100;
                    Console.Write($"\rQuantize Werner: [{new string('#', currentImage)}{new string(' ', totalImages - currentImage)}] {progress:F2}%");
                }

            }
            Console.WriteLine("\nQuantize Werner abgeschlossen!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fehler während der Resize {ex.Message}");
        }
    }

    public static void FotoSepia(List<string> dataPath, string directoryPath)
    {
        try
        {
            int totalImages = dataPath.Count;
            int currentImage = 0;
            foreach (var item in dataPath)
            {
                string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(item);
                //string outputFileName = $"{fileNameWithoutExtension}_R.png";


                using (Image<Rgba32> image = Image.Load<Rgba32>(item))
                {

                    if (image.Height != 0 && image.Width != 0)
                    {
                        using (var resizedImage = image.Clone(x => x.Sepia()))
                        {
                            string outputFileName = $"{fileNameWithoutExtension}_Sepia.png";
                            string outputFilePath = Path.Combine(directoryPath, outputFileName);

                            resizedImage.Save(outputFilePath);
                        }

                    }
                    else
                    {
                        continue;
                    }
                    currentImage++;
                    double progress = (double)currentImage / totalImages * 100;
                    Console.Write($"\rSepia: [{new string('#', currentImage)}{new string(' ', totalImages - currentImage)}] {progress:F2}%");
                }

            }
            Console.WriteLine("\nSepia abgeschlossen!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fehler während der Resize {ex.Message}");
        }
    }

    public static void FotoVignette(List<string> dataPath, string directoryPath)
    {
        try
        {
            int totalImages = dataPath.Count;
            int currentImage = 0;
            foreach (var item in dataPath)
            {
                string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(item);
                //string outputFileName = $"{fileNameWithoutExtension}_R.png";


                using (Image<Rgba32> image = Image.Load<Rgba32>(item))
                {

                    if (image.Height != 0 && image.Width != 0)
                    {
                        using (var resizedImage = image.Clone(x => x.Vignette()))
                        {
                            string outputFileName = $"{fileNameWithoutExtension}_Vignette.png";
                            string outputFilePath = Path.Combine(directoryPath, outputFileName);

                            resizedImage.Save(outputFilePath);
                        }

                    }
                    else
                    {
                        continue;
                    }
                    currentImage++;
                    double progress = (double)currentImage / totalImages * 100;
                    Console.Write($"\rVignette: [{new string('#', currentImage)}{new string(' ', totalImages - currentImage)}] {progress:F2}%");
                }

            }
            Console.WriteLine("\nVignette abgeschlossen!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fehler während der Resize {ex.Message}");
        }
    }

    public static void FotoBlackWhite(List<string> dataPath, string directoryPath)
    {
        try
        {
            int totalImages = dataPath.Count;
            int currentImage = 0;
            foreach (var item in dataPath)
            {
                string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(item);
                //string outputFileName = $"{fileNameWithoutExtension}_R.png";


                using (Image<Rgba32> image = Image.Load<Rgba32>(item))
                {

                    if (image.Height != 0 && image.Width != 0)
                    {
                        using (var resizedImage = image.Clone(x => x.BlackWhite()))
                        {
                            string outputFileName = $"{fileNameWithoutExtension}_BlackWhite.png";
                            string outputFilePath = Path.Combine(directoryPath, outputFileName);

                            resizedImage.Save(outputFilePath);
                        }

                    }
                    else
                    {
                        continue;
                    }
                    currentImage++;
                    double progress = (double)currentImage / totalImages * 100;
                    Console.Write($"\rBlackWhite: [{new string('#', currentImage)}{new string(' ', totalImages - currentImage)}] {progress:F2}%");
                }

            }
            Console.WriteLine("\nBlackWhite abgeschlossen!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fehler während der Resize {ex.Message}");
        }
    }

    public static void FotoBokehBlur(List<string> dataPath, string directoryPath)
    {
        try
        {
            int totalImages = dataPath.Count;
            int currentImage = 0;
            foreach (var item in dataPath)
            {
                string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(item);
                //string outputFileName = $"{fileNameWithoutExtension}_R.png";


                using (Image<Rgba32> image = Image.Load<Rgba32>(item))
                {

                    if (image.Height != 0 && image.Width != 0)
                    {
                        using (var resizedImage = image.Clone(x => x.BokehBlur()))
                        {
                            string outputFileName = $"{fileNameWithoutExtension}_BokehBlur.png";
                            string outputFilePath = Path.Combine(directoryPath, outputFileName);

                            resizedImage.Save(outputFilePath);
                        }

                    }
                    else
                    {
                        continue;
                    }
                    currentImage++;
                    double progress = (double)currentImage / totalImages * 100;
                    Console.Write($"\rBokehBlur: [{new string('#', currentImage)}{new string(' ', totalImages - currentImage)}] {progress:F2}%");
                }

            }
            Console.WriteLine("\nBokehBlur abgeschlossen!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fehler während der Resize {ex.Message}");
        }
    }

    public static void FotoBoxBlur(List<string> dataPath, string directoryPath)
    {
        try
        {
            int totalImages = dataPath.Count;
            int currentImage = 0;
            foreach (var item in dataPath)
            {
                string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(item);
                //string outputFileName = $"{fileNameWithoutExtension}_R.png";


                using (Image<Rgba32> image = Image.Load<Rgba32>(item))
                {

                    if (image.Height != 0 && image.Width != 0)
                    {
                        using (var resizedImage = image.Clone(x => x.BoxBlur()))
                        {
                            string outputFileName = $"{fileNameWithoutExtension}_BoxBlur.png";
                            string outputFilePath = Path.Combine(directoryPath, outputFileName);

                            resizedImage.Save(outputFilePath);
                        }

                    }
                    else
                    {
                        continue;
                    }
                    currentImage++;
                    double progress = (double)currentImage / totalImages * 100;
                    Console.Write($"\rBoxBlur: [{new string('#', currentImage)}{new string(' ', totalImages - currentImage)}] {progress:F2}%");
                }

            }
            Console.WriteLine("\nBoxBlur abgeschlossen!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fehler während der Resize {ex.Message}");
        }
    }

    public static void FotoDetectEdges(List<string> dataPath, string directoryPath)
    {
        try
        {
            int totalImages = dataPath.Count;
            int currentImage = 0;
            foreach (var item in dataPath)
            {
                string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(item);
                //string outputFileName = $"{fileNameWithoutExtension}_R.png";


                using (Image<Rgba32> image = Image.Load<Rgba32>(item))
                {

                    if (image.Height != 0 && image.Width != 0)
                    {
                        using (var resizedImage = image.Clone(x => x.DetectEdges()))
                        {
                            string outputFileName = $"{fileNameWithoutExtension}_DetectEdges.png";
                            string outputFilePath = Path.Combine(directoryPath, outputFileName);

                            resizedImage.Save(outputFilePath);
                        }

                    }
                    else
                    {
                        continue;
                    }
                    currentImage++;
                    double progress = (double)currentImage / totalImages * 100;
                    Console.Write($"\rDetectEdges: [{new string('#', currentImage)}{new string(' ', totalImages - currentImage)}] {progress:F2}%");
                }

            }
            Console.WriteLine("\nDetectEdges abgeschlossen!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fehler während der Resize {ex.Message}");
        }
    }

    public static void FotoDither(List<string> dataPath, string directoryPath)
    {
        try
        {
            int totalImages = dataPath.Count;
            int currentImage = 0;
            foreach (var item in dataPath)
            {
                string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(item);
                //string outputFileName = $"{fileNameWithoutExtension}_R.png";


                using (Image<Rgba32> image = Image.Load<Rgba32>(item))
                {

                    if (image.Height != 0 && image.Width != 0)
                    {
                        using (var resizedImage = image.Clone(x => x.Dither()))
                        {
                            string outputFileName = $"{fileNameWithoutExtension}_Dither.png";
                            string outputFilePath = Path.Combine(directoryPath, outputFileName);

                            resizedImage.Save(outputFilePath);
                        }

                    }
                    else
                    {
                        continue;
                    }
                    currentImage++;
                    double progress = (double)currentImage / totalImages * 100;
                    Console.Write($"\rDither: [{new string('#', currentImage)}{new string(' ', totalImages - currentImage)}] {progress:F2}%");
                }

            }
            Console.WriteLine("\nDither abgeschlossen!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fehler während der Resize {ex.Message}");
        }
    }

    public static void FotoEntropyCrop(List<string> dataPath, string directoryPath)
    {
        try
        {
            int totalImages = dataPath.Count;
            int currentImage = 0;
            foreach (var item in dataPath)
            {
                string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(item);
                //string outputFileName = $"{fileNameWithoutExtension}_R.png";


                using (Image<Rgba32> image = Image.Load<Rgba32>(item))
                {

                    if (image.Height != 0 && image.Width != 0)
                    {
                        using (var resizedImage = image.Clone(x => x.EntropyCrop()))
                        {
                            string outputFileName = $"{fileNameWithoutExtension}_EntropyCrop.png";
                            string outputFilePath = Path.Combine(directoryPath, outputFileName);

                            resizedImage.Save(outputFilePath);
                        }

                    }
                    else
                    {
                        continue;
                    }
                    currentImage++;
                    double progress = (double)currentImage / totalImages * 100;
                    Console.Write($"\rEntropyCrop: [{new string('#', currentImage)}{new string(' ', totalImages - currentImage)}] {progress:F2}%");
                }

            }
            Console.WriteLine("\nEntropyCrop abgeschlossen!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fehler während der Resize {ex.Message}");
        }
    }

    public static void FotoGlow(List<string> dataPath, string directoryPath)
    {
        try
        {
            int totalImages = dataPath.Count;
            int currentImage = 0;
            foreach (var item in dataPath)
            {
                string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(item);
                //string outputFileName = $"{fileNameWithoutExtension}_R.png";


                using (Image<Rgba32> image = Image.Load<Rgba32>(item))
                {

                    if (image.Height != 0 && image.Width != 0)
                    {
                        using (var resizedImage = image.Clone(x => x.Glow()))
                        {
                            string outputFileName = $"{fileNameWithoutExtension}_Glow.png";
                            string outputFilePath = Path.Combine(directoryPath, outputFileName);

                            resizedImage.Save(outputFilePath);
                        }

                    }
                    else
                    {
                        continue;
                    }
                    currentImage++;
                    double progress = (double)currentImage / totalImages * 100;
                    Console.Write($"\rGlow: [{new string('#', currentImage)}{new string(' ', totalImages - currentImage)}] {progress:F2}%");
                }

            }
            Console.WriteLine("\nGlow abgeschlossen!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fehler während der Resize {ex.Message}");
        }
    }

    public static void FotoHistogramEqualization(List<string> dataPath, string directoryPath)
    {
        try
        {
            int totalImages = dataPath.Count;
            int currentImage = 0;
            foreach (var item in dataPath)
            {
                string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(item);
                //string outputFileName = $"{fileNameWithoutExtension}_R.png";


                using (Image<Rgba32> image = Image.Load<Rgba32>(item))
                {

                    if (image.Height != 0 && image.Width != 0)
                    {
                        using (var resizedImage = image.Clone(x => x.HistogramEqualization()))
                        {
                            string outputFileName = $"{fileNameWithoutExtension}_HistogramEqualization.png";
                            string outputFilePath = Path.Combine(directoryPath, outputFileName);

                            resizedImage.Save(outputFilePath);
                        }

                    }
                    else
                    {
                        continue;
                    }
                    currentImage++;
                    double progress = (double)currentImage / totalImages * 100;
                    Console.Write($"\rHistogramEqualization: [{new string('#', currentImage)}{new string(' ', totalImages - currentImage)}] {progress:F2}%");
                }

            }
            Console.WriteLine("\nHistogramEqualization abgeschlossen!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fehler während der Resize {ex.Message}");
        }
    }

    public static void FotoInvert(List<string> dataPath, string directoryPath)
    {
        try
        {
            int totalImages = dataPath.Count;
            int currentImage = 0;
            foreach (var item in dataPath)
            {
                string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(item);
                //string outputFileName = $"{fileNameWithoutExtension}_R.png";


                using (Image<Rgba32> image = Image.Load<Rgba32>(item))
                {

                    if (image.Height != 0 && image.Width != 0)
                    {
                        using (var resizedImage = image.Clone(x => x.Invert()))
                        {
                            string outputFileName = $"{fileNameWithoutExtension}_Invert.png";
                            string outputFilePath = Path.Combine(directoryPath, outputFileName);

                            resizedImage.Save(outputFilePath);
                        }

                    }
                    else
                    {
                        continue;
                    }
                    currentImage++;
                    double progress = (double)currentImage / totalImages * 100;
                    Console.Write($"\rInvert: [{new string('#', currentImage)}{new string(' ', totalImages - currentImage)}] {progress:F2}%");
                }

            }
            Console.WriteLine("\nInvert abgeschlossen!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fehler während der Resize {ex.Message}");
        }
    }

    public static void FotoKodachrome(List<string> dataPath, string directoryPath)
    {
        try
        {
            int totalImages = dataPath.Count;
            int currentImage = 0;
            foreach (var item in dataPath)
            {
                string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(item);
                //string outputFileName = $"{fileNameWithoutExtension}_R.png";


                using (Image<Rgba32> image = Image.Load<Rgba32>(item))
                {

                    if (image.Height != 0 && image.Width != 0)
                    {
                        using (var resizedImage = image.Clone(x => x.Kodachrome()))
                        {
                            string outputFileName = $"{fileNameWithoutExtension}_Kodachrome.png";
                            string outputFilePath = Path.Combine(directoryPath, outputFileName);

                            resizedImage.Save(outputFilePath);
                        }

                    }
                    else
                    {
                        continue;
                    }
                    currentImage++;
                    double progress = (double)currentImage / totalImages * 100;
                    Console.Write($"\rKodachrome: [{new string('#', currentImage)}{new string(' ', totalImages - currentImage)}] {progress:F2}%");
                }

            }
            Console.WriteLine("\nKodachrome abgeschlossen!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fehler während der Resize {ex.Message}");
        }
    }

    public static void FotoLomograph(List<string> dataPath, string directoryPath)
    {
        try
        {
            int totalImages = dataPath.Count;
            int currentImage = 0;
            foreach (var item in dataPath)
            {
                string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(item);
                //string outputFileName = $"{fileNameWithoutExtension}_R.png";


                using (Image<Rgba32> image = Image.Load<Rgba32>(item))
                {

                    if (image.Height != 0 && image.Width != 0)
                    {
                        using (var resizedImage = image.Clone(x => x.Lomograph()))
                        {
                            string outputFileName = $"{fileNameWithoutExtension}_Lomograph.png";
                            string outputFilePath = Path.Combine(directoryPath, outputFileName);

                            resizedImage.Save(outputFilePath);
                        }

                    }
                    else
                    {
                        continue;
                    }
                    currentImage++;
                    double progress = (double)currentImage / totalImages * 100;
                    Console.Write($"\rLomograph: [{new string('#', currentImage)}{new string(' ', totalImages - currentImage)}] {progress:F2}%");
                }

            }
            Console.WriteLine("\nLomograph abgeschlossen!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fehler während der Resize {ex.Message}");
        }
    }

    public static void FotoOilPaint(List<string> dataPath, string directoryPath)
    {
        try
        {
            int totalImages = dataPath.Count;
            int currentImage = 0;
            foreach (var item in dataPath)
            {
                string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(item);
                //string outputFileName = $"{fileNameWithoutExtension}_R.png";


                using (Image<Rgba32> image = Image.Load<Rgba32>(item))
                {

                    if (image.Height != 0 && image.Width != 0)
                    {
                        using (var resizedImage = image.Clone(x => x.OilPaint()))
                        {
                            string outputFileName = $"{fileNameWithoutExtension}_OilPaint.png";
                            string outputFilePath = Path.Combine(directoryPath, outputFileName);

                            resizedImage.Save(outputFilePath);
                        }

                    }
                    else
                    {
                        continue;
                    }
                    currentImage++;
                    double progress = (double)currentImage / totalImages * 100;
                    Console.Write($"\rOilPaint: [{new string('#', currentImage)}{new string(' ', totalImages - currentImage)}] {progress:F2}%");
                }

            }
            Console.WriteLine("\nOilPaint abgeschlossen!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fehler während der Resize {ex.Message}");
        }
    }

    public static void FotoPixelate(List<string> dataPath, string directoryPath)
    {
        try
        {
            int totalImages = dataPath.Count;
            int currentImage = 0;
            foreach (var item in dataPath)
            {
                string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(item);
                //string outputFileName = $"{fileNameWithoutExtension}_R.png";


                using (Image<Rgba32> image = Image.Load<Rgba32>(item))
                {

                    if (image.Height != 0 && image.Width != 0)
                    {
                        using (var resizedImage = image.Clone(x => x.Pixelate()))
                        {
                            string outputFileName = $"{fileNameWithoutExtension}_Pixelate.png";
                            string outputFilePath = Path.Combine(directoryPath, outputFileName);

                            resizedImage.Save(outputFilePath);
                        }

                    }
                    else
                    {
                        continue;
                    }
                    currentImage++;
                    double progress = (double)currentImage / totalImages * 100;
                    Console.Write($"\rPixelate: [{new string('#', currentImage)}{new string(' ', totalImages - currentImage)}] {progress:F2}%");
                }

            }
            Console.WriteLine("\nPixelate abgeschlossen!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fehler während der Resize {ex.Message}");
        }
    }

    public static void FotoPolaroid(List<string> dataPath, string directoryPath)
    {
        try
        {
            int totalImages = dataPath.Count;
            int currentImage = 0;
            foreach (var item in dataPath)
            {
                string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(item);
                //string outputFileName = $"{fileNameWithoutExtension}_R.png";


                using (Image<Rgba32> image = Image.Load<Rgba32>(item))
                {

                    if (image.Height != 0 && image.Width != 0)
                    {
                        using (var resizedImage = image.Clone(x => x.Polaroid()))
                        {
                            string outputFileName = $"{fileNameWithoutExtension}_Polaroid.png";
                            string outputFilePath = Path.Combine(directoryPath, outputFileName);

                            resizedImage.Save(outputFilePath);
                        }

                    }
                    else
                    {
                        continue;
                    }
                    currentImage++;
                    double progress = (double)currentImage / totalImages * 100;
                    Console.Write($"\rPolaroid: [{new string('#', currentImage)}{new string(' ', totalImages - currentImage)}] {progress:F2}%");
                }

            }
            Console.WriteLine("\nPolaroid abgeschlossen!");
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
        Console.WriteLine("Achtung! 1 Bild += 52");
        while (arbeit)
        {

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

