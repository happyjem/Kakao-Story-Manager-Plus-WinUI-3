﻿using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;
using System;
using Windows.UI;

namespace KSMP.Utils
{
    public static class Common
    {
        public static BitmapImage GenerateImageUrlSource(string url)
        {
            var imageUrl = new Uri(url);
            var bitmap = new BitmapImage
            {
                UriSource = imageUrl
            };
            return bitmap;
        }

        public static SolidColorBrush GetColorFromHexa(string hexaColor)
        {
            return new SolidColorBrush(
                Color.FromArgb(
                    Convert.ToByte(hexaColor.Substring(1, 2), 16),
                    Convert.ToByte(hexaColor.Substring(3, 2), 16),
                    Convert.ToByte(hexaColor.Substring(5, 2), 16),
                    Convert.ToByte(hexaColor.Substring(7, 2), 16)
                )
            );
        }
    }
}
