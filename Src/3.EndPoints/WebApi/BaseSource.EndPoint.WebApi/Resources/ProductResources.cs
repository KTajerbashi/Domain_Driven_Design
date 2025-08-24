namespace BaseSource.EndPoint.WebApi.Resources;


public enum ProductResourcesEnum : byte
{
    CreateSmartphone,
    CreateLaptop,
    CreateSmartwatch,
    CreateHeadphones,
    CreateTablet,
}

public static class ProductResources
{
    private static string CreateSmartphone => @"
{
  ""productCategory"": {
    ""name"": ""Smartphones"",
    ""slug"": ""smartphones"",
    ""parentCategoryId"": 1
  },
  ""productWeight"": {
    ""value"": 210,
    ""weightUnit"": 0
  },
  ""productDimensions"": {
    ""length"": 15.9,
    ""width"": 7.5,
    ""height"": 0.82,
    ""unit"": 0
  },
  ""productDetails"": [
    { ""key"": ""Brand"", ""value"": ""Samsung"" },
    { ""key"": ""Model"", ""value"": ""Galaxy S25 Ultra"" },
    { ""key"": ""Storage"", ""value"": ""512GB"" },
    { ""key"": ""Color"", ""value"": ""Phantom Black"" },
    { ""key"": ""Screen Size"", ""value"": ""6.8 inches"" },
    { ""key"": ""Display"", ""value"": ""Dynamic AMOLED 2X"" },
    { ""key"": ""Resolution"", ""value"": ""3200 x 1440 pixels"" },
    { ""key"": ""Processor"", ""value"": ""Snapdragon 8 Gen 4"" },
    { ""key"": ""RAM"", ""value"": ""12GB"" },
    { ""key"": ""Rear Camera"", ""value"": ""200MP + 12MP + 10MP + 10MP"" },
    { ""key"": ""Front Camera"", ""value"": ""40MP"" },
    { ""key"": ""Battery"", ""value"": ""5000 mAh"" },
    { ""key"": ""Charging"", ""value"": ""USB-C, Fast Charge 65W, Wireless 25W"" },
    { ""key"": ""Operating System"", ""value"": ""Android 15"" },
    { ""key"": ""Connectivity"", ""value"": ""5G, Wi-Fi 7, Bluetooth 5.4"" },
    { ""key"": ""Water Resistance"", ""value"": ""IP68"" },
    { ""key"": ""Stylus"", ""value"": ""S Pen included"" }
  ],
  ""title"": ""Samsung Galaxy S25 Ultra"",
  ""description"": ""The next-gen Samsung flagship with 200MP camera and advanced AI features."",
  ""price"": 1399.99,
  ""sku"": ""SGS25U-512GB-BLK""
}

";
    private static string CreateLaptop => @"
{
  ""productCategory"": {
    ""name"": ""Laptops"",
    ""slug"": ""laptops"",
    ""parentCategoryId"": 2
  },
  ""productWeight"": {
    ""value"": 1400,
    ""weightUnit"": 0
  },
  ""productDimensions"": {
    ""length"": 31.5,
    ""width"": 22.0,
    ""height"": 1.5,
    ""unit"": 0
  },
  ""productDetails"": [
    { ""key"": ""Brand"", ""value"": ""Dell"" },
    { ""key"": ""Model"", ""value"": ""XPS 15 2025"" },
    { ""key"": ""Processor"", ""value"": ""Intel Core Ultra 9"" },
    { ""key"": ""RAM"", ""value"": ""32GB DDR5"" },
    { ""key"": ""Storage"", ""value"": ""1TB SSD"" },
    { ""key"": ""GPU"", ""value"": ""NVIDIA RTX 4070"" },
    { ""key"": ""Screen Size"", ""value"": ""15.6 inches"" },
    { ""key"": ""Display"", ""value"": ""OLED 4K Touch"" },
    { ""key"": ""Battery"", ""value"": ""97 Wh"" },
    { ""key"": ""Operating System"", ""value"": ""Windows 12"" },
    { ""key"": ""Ports"", ""value"": ""2x Thunderbolt 4, HDMI, SD card slot"" },
    { ""key"": ""Keyboard"", ""value"": ""Backlit, Fingerprint Reader"" }
  ],
  ""title"": ""Dell XPS 15 (2025)"",
  ""description"": ""High-performance laptop with OLED display and dedicated RTX graphics."",
  ""price"": 2499.99,
  ""sku"": ""DELL-XPS15-1TB-RTX4070""
}

";
    private static string CreateSmartwatch => @"
{
  ""productCategory"": {
    ""name"": ""Smartwatches"",
    ""slug"": ""smartwatches"",
    ""parentCategoryId"": 3
  },
  ""productWeight"": {
    ""value"": 45,
    ""weightUnit"": 0
  },
  ""productDimensions"": {
    ""length"": 4.5,
    ""width"": 4.5,
    ""height"": 1.1,
    ""unit"": 0
  },
  ""productDetails"": [
    { ""key"": ""Brand"", ""value"": ""Apple"" },
    { ""key"": ""Model"", ""value"": ""Apple Watch Series 10"" },
    { ""key"": ""Case Size"", ""value"": ""45mm"" },
    { ""key"": ""Display"", ""value"": ""Always-On Retina LTPO OLED"" },
    { ""key"": ""Battery Life"", ""value"": ""18 hours"" },
    { ""key"": ""Water Resistance"", ""value"": ""50m"" },
    { ""key"": ""Sensors"", ""value"": ""Heart Rate, Blood Oxygen, ECG, Temperature"" },
    { ""key"": ""Connectivity"", ""value"": ""Wi-Fi, LTE, Bluetooth 5.3"" },
    { ""key"": ""Operating System"", ""value"": ""watchOS 11"" }
  ],
  ""title"": ""Apple Watch Series 10"",
  ""description"": ""Advanced smartwatch with health monitoring and LTE connectivity."",
  ""price"": 499.99,
  ""sku"": ""AWATCH10-45MM-LTE""
}
";
    private static string CreateHeadphones => @"
{
  ""productCategory"": {
    ""name"": ""Headphones"",
    ""slug"": ""headphones"",
    ""parentCategoryId"": 4
  },
  ""productWeight"": {
    ""value"": 260,
    ""weightUnit"": 0
  },
  ""productDimensions"": {
    ""length"": 18.5,
    ""width"": 16.0,
    ""height"": 8.0,
    ""unit"": 0
  },
  ""productDetails"": [
    { ""key"": ""Brand"", ""value"": ""Sony"" },
    { ""key"": ""Model"", ""value"": ""WH-1000XM6"" },
    { ""key"": ""Type"", ""value"": ""Over-Ear"" },
    { ""key"": ""Connectivity"", ""value"": ""Bluetooth 5.4, USB-C"" },
    { ""key"": ""Noise Cancellation"", ""value"": ""Adaptive ANC"" },
    { ""key"": ""Battery Life"", ""value"": ""40 hours"" },
    { ""key"": ""Charging"", ""value"": ""Quick Charge (10 min = 5 hrs)"" },
    { ""key"": ""Microphone"", ""value"": ""Yes, AI Noise Reduction"" },
    { ""key"": ""Compatibility"", ""value"": ""iOS, Android, Windows"" }
  ],
  ""title"": ""Sony WH-1000XM6"",
  ""description"": ""Premium wireless headphones with adaptive noise cancellation and long battery life."",
  ""price"": 399.99,
  ""sku"": ""SONY-WH1000XM6-BLK""
}

";
    private static string CreateTablet => @"
{
  ""productCategory"": {
    ""name"": ""Tablets"",
    ""slug"": ""tablets"",
    ""parentCategoryId"": 5
  },
  ""productWeight"": {
    ""value"": 650,
    ""weightUnit"": 0
  },
  ""productDimensions"": {
    ""length"": 28.1,
    ""width"": 21.5,
    ""height"": 0.6,
    ""unit"": 0
  },
  ""productDetails"": [
    { ""key"": ""Brand"", ""value"": ""Apple"" },
    { ""key"": ""Model"", ""value"": ""iPad Pro 14-inch (2025)"" },
    { ""key"": ""Processor"", ""value"": ""Apple M4"" },
    { ""key"": ""Storage"", ""value"": ""512GB"" },
    { ""key"": ""RAM"", ""value"": ""16GB"" },
    { ""key"": ""Display"", ""value"": ""Liquid Retina XDR Mini-LED"" },
    { ""key"": ""Resolution"", ""value"": ""3024 x 1964 pixels"" },
    { ""key"": ""Battery"", ""value"": ""12,000 mAh"" },
    { ""key"": ""Operating System"", ""value"": ""iPadOS 19"" },
    { ""key"": ""Connectivity"", ""value"": ""5G, Wi-Fi 7, Bluetooth 5.4"" },
    { ""key"": ""Accessories"", ""value"": ""Apple Pencil 3, Magic Keyboard"" }
  ],
  ""title"": ""Apple iPad Pro 14 (2025)"",
  ""description"": ""Next-gen iPad Pro with M4 chip and high-resolution XDR display."",
  ""price"": 1799.99,
  ""sku"": ""IPADPRO14-M4-512GB""
}

";


    public static string Handler(ProductResourcesEnum @enum)
    {
        return @enum switch
        {
            ProductResourcesEnum.CreateSmartphone => CreateSmartphone,
            ProductResourcesEnum.CreateLaptop => CreateLaptop,
            ProductResourcesEnum.CreateSmartwatch => CreateSmartwatch,
            ProductResourcesEnum.CreateHeadphones => CreateHeadphones,
            ProductResourcesEnum.CreateTablet => CreateTablet,
            _ => "Not Selected Correct Item !!!"
        };
    }
}
