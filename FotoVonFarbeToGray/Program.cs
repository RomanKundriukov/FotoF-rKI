﻿using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

public static class Program
{
    #region HilfsMethoden

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
    #endregion

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
                                string outputFileName = $"{fileNameWithoutExtension}_Resize{scaleFactor}.png";
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
                    Console.Write($"\rResize: [{new string('#', (int)(progress / 4))}{new string(' ', 25 - (int)(progress / 4))}] {progress:F2}%");
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
                                string outputFileName = $"{fileNameWithoutExtension}_Rotation{rotateFactor}.png";
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
                    Console.Write($"\rRotation: [{new string('#', (int)(progress / 4))}{new string(' ', 25 - (int)(progress / 4))}] {progress:F2}%");
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
                            string outputFileName = $"{fileNameWithoutExtension}_FlipHorisontal.png";
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
                    Console.Write($"\rFlip Horisontal: [{new string('#', (int)(progress / 4))}{new string(' ', 25 - (int)(progress / 4))}] {progress:F2}%");
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
                            string outputFileName = $"{fileNameWithoutExtension}_FlipVertical.png";
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
                    Console.Write($"\rFlip Vertical: [{new string('#', (int)(progress / 4))}{new string(' ', 25 - (int)(progress / 4))}] {progress:F2}%");
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
                            string outputFileName = $"{fileNameWithoutExtension}_FarbeGray.png";
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
                    Console.Write($"\rFarbe Gray: [{new string('#', (int)(progress / 4))}{new string(' ', 25 - (int)(progress / 4))}] {progress:F2}%");
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
                                string outputFileName = $"{fileNameWithoutExtension}_BrightnessDunkel{_brightness}.png";
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
                    Console.Write($"\rBrightness Dunkel: [{new string('#', (int)(progress / 4))}{new string(' ', 25 - (int)(progress / 4))}] {progress:F2}%");
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
                                string outputFileName = $"{fileNameWithoutExtension}_BrightnessHell{_brightness}.png";
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
                    Console.Write($"\rBrightness Hell: [{new string('#', (int)(progress / 4))}{new string(' ', 25 - (int)(progress / 4))}] {progress:F2}%");
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
                                string outputFileName = $"{fileNameWithoutExtension}_ContrastUnten{Contrast}.png";
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
                    Console.Write($"\rContrast niedrig: [{new string('#', (int)(progress / 4))}{new string(' ', 25 - (int)(progress / 4))}] {progress:F2}%");
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
                                string outputFileName = $"{fileNameWithoutExtension}_ContrastOben{Contrast}.png";
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
                    Console.Write($"\rContrast hoch: [{new string('#', (int)(progress / 4))}{new string(' ', 25 - (int)(progress / 4))}] {progress:F2}%");
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
                                string outputFileName = $"{fileNameWithoutExtension}_SaturateUnten{Saturate}.png";
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
                    Console.Write($"\rSaturate niedrig: [{new string('#', (int)(progress / 4))}{new string(' ', 25 - (int)(progress / 4))}] {progress:F2}%");
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
                                string outputFileName = $"{fileNameWithoutExtension}_SaturateOben{Saturate}.png";
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
                    Console.Write($"\rSaturate hoch: [{new string('#', (int)(progress / 4))}{new string(' ', 25 - (int)(progress / 4))}] {progress:F2}%");
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
                            string outputFileName = $"{fileNameWithoutExtension}_GaussianBlur.png";
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
                    Console.Write($"\rGaussian Blur: [{new string('#', (int)(progress / 4))}{new string(' ', 25 - (int)(progress / 4))}] {progress:F2}%");
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
                            string outputFileName = $"{fileNameWithoutExtension}_GaussianSharpen.png";
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
                    Console.Write($"\rGaussian Sharpen: [{new string('#', (int)(progress / 4))}{new string(' ', 25 - (int)(progress / 4))}] {progress:F2}%");
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
                                string outputFileName = $"{fileNameWithoutExtension}_SkewPlus{skewAmount}.png";
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
                    Console.Write($"\rSkew Plus: [{new string('#', (int)(progress / 4))}{new string(' ', 25 - (int)(progress / 4))}] {progress:F2}%");
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
                                string outputFileName = $"{fileNameWithoutExtension}_SkewMinus{skewAmount}.png";
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
                    Console.Write($"\rSkew Minus: [{new string('#', (int)(progress / 4))}{new string(' ', 25 - (int)(progress / 4))}] {progress:F2}%");
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
                            string outputFileName = $"{fileNameWithoutExtension}_AdaptiveThreshold.png";
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
                    Console.Write($"\rAdaptive Threshold: [{new string('#', (int)(progress / 4))}{new string(' ', 25 - (int)(progress / 4))}] {progress:F2}%");
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
                            string outputFileName = $"{fileNameWithoutExtension}_QuantizeWebSafe.png";
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
                    Console.Write($"\rQuantize WebSafe: [{new string('#', (int)(progress / 4))}{new string(' ', 25 - (int)(progress / 4))}] {progress:F2}%");
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
                            string outputFileName = $"{fileNameWithoutExtension}_QuantizeOctree.png";
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
                    Console.Write($"\rQuantize Octree: [{new string('#', (int)(progress / 4))}{new string(' ', 25 - (int)(progress / 4))}] {progress:F2}%");
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
                            string outputFileName = $"{fileNameWithoutExtension}_QuantizeWu.png";
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
                            string outputFileName = $"{fileNameWithoutExtension}_QuantizeWerner.png";
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
                    Console.Write($"\rQuantize Werner: [{new string('#', (int)(progress / 4))}{new string(' ', 25 - (int)(progress / 4))}] {progress:F2}%");
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
                    Console.Write($"\rSepia: [{new string('#', (int)(progress / 4))}{new string(' ', 25 - (int)(progress / 4))}] {progress:F2}%");
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
                    Console.Write($"\rVignette: [{new string('#', (int)(progress / 4))}{new string(' ', 25 - (int)(progress / 4))}] {progress:F2}%");
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
                    Console.Write($"\rBlackWhite: [{new string('#', (int)(progress / 4))}{new string(' ', 25 - (int)(progress / 4))}] {progress:F2}%");
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
                    Console.Write($"\rBokehBlur: [{new string('#', (int)(progress / 4))}{new string(' ', 25 - (int)(progress / 4))}] {progress:F2}%");
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
                    Console.Write($"\rBoxBlur: [{new string('#', (int)(progress / 4))}{new string(' ', 25 - (int)(progress / 4))}] {progress:F2}%");
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
                    Console.Write($"\rDetectEdges: [{new string('#', (int)(progress / 4))}{new string(' ', 25 - (int)(progress / 4))}] {progress:F2}%");
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
                    Console.Write($"\rDither: [{new string('#', (int)(progress / 4))}{new string(' ', 25 - (int)(progress / 4))}] {progress:F2}%");
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
                    Console.Write($"\rEntropyCrop: [{new string('#', (int)(progress / 4))}{new string(' ', 25 - (int)(progress / 4))}] {progress:F2}%");
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
                    Console.Write($"\rGlow: [{new string('#', (int)(progress / 4))}{new string(' ', 25 - (int)(progress / 4))}] {progress:F2}%");
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
                    Console.Write($"\rHistogramEqualization: [{new string('#', (int)(progress / 4))}{new string(' ', 25 - (int)(progress / 4))}] {progress:F2}%");
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
                    Console.Write($"\rInvert: [{new string('#', (int)(progress / 4))}{new string(' ', 25 - (int)(progress / 4))}] {progress:F2}%");
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
                    Console.Write($"\rKodachrome: [{new string('#', (int)(progress / 4))}{new string(' ', 25 - (int)(progress / 4))}] {progress:F2}%");
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
                    Console.Write($"\rLomograph: [{new string('#', (int)(progress / 4))}{new string(' ', 25 - (int)(progress / 4))}] {progress:F2}%");
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
                    Console.Write($"\rOilPaint: [{new string('#', (int)(progress / 4))}{new string(' ', 25 - (int)(progress / 4))}] {progress:F2}%");
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
                    Console.Write($"\rPixelate: [{new string('#', (int)(progress / 4))}{new string(' ', 25 - (int)(progress / 4))}] {progress:F2}%");
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
                    Console.Write($"\rPolaroid: [{new string('#', (int)(progress / 4))}{new string(' ', 25 - (int)(progress / 4))}] {progress:F2}%");
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

    public static void Recomended(string path)
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
                Console.WriteLine($"{path}\n");
                FotoResize(_files, path);
                FotoRotation(_files, path);
                FotoFlipHorisontal(_files, path);
                FotoFlipVertical(_files, path);
                FotoBrightnessDunkel(_files, path);
                FotoBrightnessHell(_files, path);
                FotoContrastOben(_files, path);
                FotoContrastUnten(_files, path);
                FotoGaussianBlur(_files, path);
                FotoSaturateOben(_files, path);
                FotoSaturateUnten(_files, path);
                FotoSepia(_files, path);
                FotoBlackWhite(_files, path);
                FotoInvert(_files, path);
                FotoDetectEdges(_files, path);
                FotoEntropyCrop(_files, path);
                FotoHistogramEqualization(_files, path);
                FotoSkewPlus(_files, path);
                FotoSkewMinus(_files, path);

            }
            else
            {
                Parallel.ForEach(_directory, item =>
                {
                    Console.WriteLine($"{item}\n");
                    List<string> _dirFiles = new();
                    _dirFiles = GetAllFiles(item);

                    FotoResize(_dirFiles, item);
                    FotoRotation(_dirFiles, item);
                    FotoFlipHorisontal(_dirFiles, item);
                    FotoFlipVertical(_dirFiles, item);
                    FotoBrightnessDunkel(_dirFiles, item);
                    FotoBrightnessHell(_dirFiles, item);
                    FotoContrastOben(_dirFiles, item);
                    FotoContrastUnten(_dirFiles, item);
                    FotoGaussianBlur(_dirFiles, item);
                    FotoSaturateOben(_dirFiles, item);
                    FotoSaturateUnten(_dirFiles, item);
                    FotoSepia(_dirFiles, item);
                    FotoBlackWhite(_dirFiles, item);
                    FotoInvert(_dirFiles, item);
                    FotoDetectEdges(_dirFiles, item);
                    FotoEntropyCrop(_dirFiles, item);
                    FotoHistogramEqualization(_dirFiles, item);
                    FotoSkewPlus(_dirFiles, item);
                    FotoSkewMinus(_dirFiles, item);
                });


            }

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    public static void Selber(string path)
    {

        List<int>? numberList = new();
        string? input;

        Console.WriteLine("Gib Zahlen ein. Drücke Enter ohne Eingabe, um zu beenden.");

        Console.WriteLine("1. Resize\r\n2. Rotation\r\n3. FlipHorisontal\r\n4. FlipVertical\r\n5. FrabeGray\r\n6. Brightness\r\n7. Contrast\r\n8. Saturate\r\n9. Gaussian\r\n10. Skew\r\n11. Adaptive\r\n12. QuantizeWebSafe\r\n13. QuantizeOctree\r\n14. QuantizeWu\r\n15. QuantizeWerner\r\n16. Sepia\r\n17. Vignette\r\n18. BlackWhite\r\n19. BokenBlur\r\n20. BoxBlur\r\n21. DetectEdges\r\n22. Dither\r\n23. EntropyCrop\r\n24. Glow\r\n25. HistogramEqualization\r\n26. Invert\r\n27. Kodachrome\r\n28. Lomograph\r\n29. Oilpaint\r\n30. Pixelate\r\n31. Polaroid");

        input = Console.ReadLine(); // Liest die Eingabe des Benutzers
        string[] inputArray = input.Split(' ');

        foreach (var item in inputArray)
        {
            if (int.TryParse(item, out int number))
            {
                numberList.Add(number); // Fügt die gültige Zahl zur Liste hinzu
            }
            else
            {
                Console.WriteLine($"Ungültige Eingabe: '{item}' wird übersprungen.");
            }
        }

        int[] numberArray = numberList.ToArray();



        try
        {
            List<string>? _directory = new();
            List<string>? _files = new();

            _directory = GetAllOrdner(path);
            _files = GetAllFiles(path);

            if (_files != null && _directory == null)
            {
                for (int i = 0; i < numberArray.Length; i++)
                {
                    int a = numberArray[i];

                    switch (a)
                    {
                        case 1:
                            FotoResize(_files, path);
                            break;
                        case 2:
                            FotoRotation(_files, path);
                            break;
                        case 3:
                            FotoFlipHorisontal(_files, path);
                            break;
                        case 4:
                            FotoFlipVertical(_files, path);
                            break;
                        case 5:
                            FotoFarbeGray(_files, path);
                            break;
                        case 6:
                            FotoBrightnessDunkel(_files, path);
                            FotoBrightnessHell(_files, path);
                            break;
                        case 7:
                            FotoContrastOben(_files, path);
                            FotoContrastUnten(_files, path);
                            break;
                        case 8:
                            FotoSaturateOben(_files, path);
                            FotoSaturateUnten(_files, path);
                            break;
                        case 9:
                            FotoGaussianBlur(_files, path);
                            FotoGaussianSharpen(_files, path);
                            break;
                        case 10:
                            FotoSkewPlus(_files, path);
                            FotoSkewMinus(_files, path);
                            break;
                        case 11:
                            FotoAdaptiveThreshold(_files, path);
                            break;
                        case 12:
                            FotoQuantizeWebSafe(_files, path);
                            break;
                        case 13:
                            FotoQuantizeOctree(_files, path);
                            break;
                        case 14:
                            FotoQuantizeWu(_files, path);
                            break;
                        case 15:
                            FotoQuantizeWerner(_files, path);
                            break;
                        case 16:
                            FotoSepia(_files, path);
                            break;
                        case 17:
                            FotoVignette(_files, path);
                            break;
                        case 18:
                            FotoBlackWhite(_files, path);
                            break;
                        case 19:
                            FotoBokehBlur(_files, path);
                            break;
                        case 20:
                            FotoBoxBlur(_files, path);
                            break;
                        case 21:
                            FotoDetectEdges(_files, path);
                            break;
                        case 22:
                            FotoDither(_files, path);
                            break;
                        case 23:
                            FotoEntropyCrop(_files, path);
                            break;
                        case 24:
                            FotoGlow(_files, path);
                            break;
                        case 25:
                            FotoHistogramEqualization(_files, path);
                            break;
                        case 26:
                            FotoInvert(_files, path);
                            break;
                        case 27:
                            FotoKodachrome(_files, path);
                            break;
                        case 28:
                            FotoLomograph(_files, path);
                            break;
                        case 29:
                            FotoOilPaint(_files, path);
                            break;
                        case 30:
                            FotoPixelate(_files, path);
                            break;
                        case 31:
                            FotoPolaroid(_files, path);
                            break;
                        default:
                            break;
                    }
                }




            }
            else
            {
                Parallel.ForEach(_directory, item =>
                {
                    List<string> _dirFiles = new();
                    _dirFiles = GetAllFiles(item);

                    for (int i = 0; i < numberArray.Length; i++)
                    {
                        int a = numberArray[i];

                        switch (a)
                        {
                            case 1:
                                FotoResize(_dirFiles, item);
                                break;
                            case 2:
                                FotoRotation(_dirFiles, item);
                                break;
                            case 3:
                                FotoFlipHorisontal(_dirFiles, item);
                                break;
                            case 4:
                                FotoFlipVertical(_dirFiles, item);
                                break;
                            case 5:
                                FotoFarbeGray(_dirFiles, item);
                                break;
                            case 6:
                                FotoBrightnessDunkel(_dirFiles, item);
                                FotoBrightnessHell(_dirFiles, item);
                                break;
                            case 7:
                                FotoContrastOben(_dirFiles, item);
                                FotoContrastUnten(_dirFiles, item);
                                break;
                            case 8:
                                FotoSaturateOben(_dirFiles, item);
                                FotoSaturateUnten(_dirFiles, item);
                                break;
                            case 9:
                                FotoGaussianBlur(_dirFiles, item);
                                FotoGaussianSharpen(_dirFiles, item);
                                break;
                            case 10:
                                FotoSkewPlus(_dirFiles, item);
                                FotoSkewMinus(_dirFiles, item);
                                break;
                            case 11:
                                FotoAdaptiveThreshold(_dirFiles, item);
                                break;
                            case 12:
                                FotoQuantizeWebSafe(_dirFiles, item);
                                break;
                            case 13:
                                FotoQuantizeOctree(_dirFiles, item);
                                break;
                            case 14:
                                FotoQuantizeWu(_dirFiles, item);
                                break;
                            case 15:
                                FotoQuantizeWerner(_dirFiles, item);
                                break;
                            case 16:
                                FotoSepia(_dirFiles, item);
                                break;
                            case 17:
                                FotoVignette(_dirFiles, item);
                                break;
                            case 18:
                                FotoBlackWhite(_dirFiles, item);
                                break;
                            case 19:
                                FotoBokehBlur(_dirFiles, item);
                                break;
                            case 20:
                                FotoBoxBlur(_dirFiles, item);
                                break;
                            case 21:
                                FotoDetectEdges(_dirFiles, item);
                                break;
                            case 22:
                                FotoDither(_dirFiles, item);
                                break;
                            case 23:
                                FotoEntropyCrop(_dirFiles, item);
                                break;
                            case 24:
                                FotoGlow(_dirFiles, item);
                                break;
                            case 25:
                                FotoHistogramEqualization(_dirFiles, item);
                                break;
                            case 26:
                                FotoInvert(_dirFiles, item);
                                break;
                            case 27:
                                FotoKodachrome(_dirFiles, item);
                                break;
                            case 28:
                                FotoLomograph(_dirFiles, item);
                                break;
                            case 29:
                                FotoOilPaint(_dirFiles, item);
                                break;
                            case 30:
                                FotoPixelate(_dirFiles, item);
                                break;
                            case 31:
                                FotoPolaroid(_dirFiles, item);
                                break;
                            default:
                                break;
                        }
                    }

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
            try
            {
                Console.WriteLine("Wählst du, was du machen möchtest:\n1. Recomended\n2. Selber entscheiden\n3. Stop");
                string input = Console.ReadLine();

                // Verarbeite die Option und überprüfe, ob es eine gültige Eingabe ist
                if (!int.TryParse(input, out int option) || option < 1 || option > 3)
                {
                    Console.WriteLine("Ungültige Option. Bitte wähle eine Zahl zwischen 1 und 3.");
                    continue;
                }

                if (option == 3)
                {
                    arbeit = false;
                    Console.WriteLine("Programm beendet.");
                    break;
                }

                // Pfadabfrage
                Console.WriteLine("Bitte gib den Pfad an:");
                string path = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(path) || !Directory.Exists(path))
                {
                    Console.WriteLine("Ungültiger Pfad. Bitte gib einen gültigen Pfad ein.");
                    continue;
                }

                // Auswahl der Option
                switch (option)
                {
                    case 1:
                        Recomended(path);
                        Console.WriteLine("Die Arbeit wurde beendet\n");
                        break;
                    case 2:
                        Selber(path);
                        Console.WriteLine("Die Arbeit wurde beendet\n");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Es ist ein Fehler aufgetreten: {ex.Message}");
            }
        }
    }
}

