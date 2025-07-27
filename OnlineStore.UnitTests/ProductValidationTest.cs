using FluentValidation;
using OnlineStore.Validations;
using OnlineStoreClient.Dto;
using OnlineStoreClient.Model;

namespace OnlineStore.UnitTests
{
    public class ProductValidationTest
    {

        [Fact]
        public void CorrectValidationProduct_Correct()
        {
            var product = new ProductDto
            {
                Name = "Valid Name",
                Description = "Valid Description",
                Price = 10.99m,
                ProductCategoryId = 1
            };
            var productValidation = new ProductValidation();

            var result = productValidation.Validate(product);

            Assert.True(result.IsValid);
        }

        [Fact]
        public void CorrectValidationProductName_Correct()
        {
            var product = new ProductDto
            {
                Name = "������� ��������� Razer Kraken X",
                Description = "Valid Description",
                Price = 10.99m,
                ProductCategoryId = 1
            };
            var productValidation = new ProductValidation();

            var result = productValidation.Validate(product);

            Assert.True(result.IsValid);
        }

        [Fact]
        public void NameIsEmpty_Failer()
        {
            var product = new ProductDto
            {
                Name = "",
                Description = "Valid Description",
                Price = 10.99m,
                ProductCategoryId = 1
            };
            var productValidation = new ProductValidation();

            var result =  productValidation.Validate(product);

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == nameof(Product.Name) &&
                                               e.ErrorMessage == "�������� ������ ����������� ��� ����������.");
        }

        [Fact]
        public void NameExceedsMaxLenght_Failer()
        {
            var product = new ProductDto
            {
                Name = "������� ASUS ZenBook 14 ������� Dell UltraSharp 27 ���������� Logitech G Pro ���� Razer DeathAdder V2 ������� ������� ���� Seagate 2TB ��������� HyperX Cloud II" +
                "���-������ Logitech C920 �������� HP 302 ������� ������ APC ������ HDMI 2.0 ������� Microsoft Surface Pro 7 ������� ���������� Razer BlackWidow ���� Logitech MX " +
                "Master 3 ������� SSD Samsung T7 1TB ������� LG 34UM69G-B ������� ��������� SteelSeries Arctis 7 ������� Canon PIXMA TS8320 ���-������ AVerMedia Live Streamer ������ " +
                "GoPro HERO9 ������������ �������� Apple AirPods Pro �������� Blue Yeti USB ������� DVD-������ ASUS USB 3.0 ���������� Corsair K95 RGB ���� SteelSeries Rival 600 " +
                "������� ������� AOC 24G2 144Hz ������� ������� ���� WD My Passport 4TB ������� ������� TP-Link Archer T3U ������������ ���������� Logitech K400 Plus ������� ���� " +
                "HyperX Pulsefire FPS, ������� ������ Defender 8 �������, ���������� Logitech K120, ���� A4Tech Bloody V8, ������� SSD Crucial X8 1TB, ������� Samsung Odyssey G5, " +
                "���-������ Microsoft LifeCam HD-3000 ������� ��������� Razer Kraken X ���� Zowie FK1 ������� ������� MSI Optix MAG241C ���������� HyperX Alloy FPS Pro",
                Description = "Valid Description",
                Price = 10.99m,
                ProductCategoryId = 1
            };
            var productValidation = new ProductValidation();

            var result = productValidation.Validate(product);

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == nameof(Product.Name) &&
                                               e.ErrorMessage == $"�������� ������ �� ������ ��������� {ProductValidation.MaxNameLength} ��������.");
        }

        [Fact]
        public void CorrectValidationProductDescriiption_Correct()
        {
            var product = new ProductDto
            {
                Name = "������� ��������� Razer Kraken X",
                Description = "������� ��������� Razer Kraken X � ��� ������ � ������� ���������, ������������� ��� ���������� ������� ������. ��� �������� 40-�� ����������," +
                "��������������� ������ � �������� ����, ��� ��������� ����� ������� ������ � �����. �������� � ��������������� ����������� ������ �������� ������, � ������" +
                "����������� ��������� ����� ������������ ���������. ��������� ���������� � ���������� �����������, ������� ��, ������� � ��������� ����������. ��������� ������" +
                "��������� � ������������� ��������, Razer Kraken X ������������ ������� ���� �� ����� ����� ����������� ���.",
                Price = 10.99m,
                ProductCategoryId = 1
            };
            var productValidation = new ProductValidation();

            var result = productValidation.Validate(product);

            Assert.True(result.IsValid);
        }

        [Fact]
        public void DescriiptionIsEmpty_Failer()
        {
            var product = new ProductDto
            {
                Name = "Name",
                Description = "",
                Price = 10.99m,
                ProductCategoryId = 1
            };
            var productValidation = new ProductValidation();

            var result = productValidation.Validate(product);

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == nameof(Product.Description) &&
                                               e.ErrorMessage == "�������� ������ ����������� ��� ����������.");
        }

        [Fact]
        public void DescriptionExceedsMaxLenght_Failer()
        {
            var product = new ProductDto
            {
                Name = "XYZ Pro 12",
                Description = "�������� XYZ Pro 12 ������������ ����� ����������� ����������, ������� �������� � ���� ��������� ���������� � �������� ������, ��� ������ ��� ��������� " +
                "������� ��� �������������, ����������� � �������� � ����������������. �� ������� 6.7-�������� AMOLED �������� � ����������� 3200 x 1440 ��������, ������� ������������ " +
                "����� � ���������� �����, � ����� �������� ������ ���� ��������� ��������� HDR10+. ������� ������� ������� Gorilla Glass Victus, ��� ����������� ��� ������������� � " +
                "������������ � ���������. �� ������ ������ ����������� ������� ������, ��������� �� �������� ������ �� 108 �� � ���������� �������������, ������������� ������ " +
                "�� 12 �� � ����� ������ 120 �������� � ��������-������ �� 12 �� � 3-������� ���������� �����, ��� ��������� ������ ������������ ������ � ����� ��������. ����������� " +
                "������ �� 32 �� ������������ ������ ���������� ������ � ������ ����� � ������� 4K, ��� ������ �� �������� ������� ��� ����� � ������������. ������ ���������� " +
                "���������� ������ ��������� XYZ A15, ������� ������������ ������� ������������������ � �������������������. � ��������� � 12 �� ����������� � 256 �� ���������� ������ " +
                "������������ ����� ��� ������� ��������� ������������ ���������� � ����. ��������� 5G ��������� ������������ ������������ ��������� ���������, ��� �������� ��������� " +
                "��� ��������� � ������-���. ����������� �������� 5000 ��� ������������ ���������� ����� ������ ��� ����������, � ��������� ������� ������� ��������� 65 �� ��������� " +
                "�������� ���������� �� 100% ����� �� 30 �����. ����� �������� ������� ������������ ������� � �������� ������������ �������, ��� ��������� �������� ������ ����������, " +
                "����� ��� �������� ��� �����-����. �������� ���������� ������������ �������� ��������� ��������������� � ��������� Dolby Atmos, ��� ������ ��� �������� ������� ��� " +
                "��������� ������ � �������. �� �������� �� ������������ ������� XYZ OS 12, ���������� �� Android, � ���������� ����������������� ���������� � ������������ ��������� " +
                "���������� ��� ����. �������������� ������� �������� ������ ���������� �������, ���������� � �������, ��������� ������������� ��� ��� �������������� ������������, � " +
                "����� ������ �� ���� � ���� �� ��������� IP68, ��� ������ ��� ��������� ��� ��������� ������ �����. �������� ����� ������������ ��� ����������� ��������� �����������, " +
                "������� Wi-Fi 6, Bluetooth 5.2 � NFC, ��� ��������� ����� ���������� ����������, ����� ��� ��������, ����� ���� � ������ �������. XYZ Pro 12 � ��� �� ������ �������, " +
                "� ������ ����������, ������� ������� ��� ���������� �� �����, ��������� � �������� ���������, � ����� ������������ ��������������� �������������.",
                Price = 10.99m,
                ProductCategoryId = 1
            };
            var productValidation = new ProductValidation();

            var result = productValidation.Validate(product);

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == nameof(Product.Description) &&
                                               e.ErrorMessage == $"�������� ������ �� ������ ��������� {ProductValidation.MaxDescriptionLength} ��������.");
        }

        [Fact]
        public void CorrectPriceProduct_Correct()
        {
            var product = new ProductDto
            {
                Name = "name",
                Description = "description",
                Price = 20532,
                ProductCategoryId = 1
            };
            var productValidation = new ProductValidation();

            var result = productValidation.Validate(product);

            Assert.True(result.IsValid);
        }

        [Fact]
        public void NotCorrectPriceProduct_Failer()
        {
            var product = new ProductDto
            {
                Name = "name",
                Description = "description",
                Price = -40,
                ProductCategoryId = 1
            };
            var productValidation = new ProductValidation();

            var result = productValidation.Validate(product);

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == nameof(Product.Price) &&
                                                e.ErrorMessage == "���� ������ ������ ���� ������ ����.");
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void NameIsEmptyTheory_Failer(string name)
        {
            var product = new ProductDto
            {
                Name = name,
                Description = "Valid Description",
                Price = 10.99m,
                ProductCategoryId = 1
            };
            var productValidation = new ProductValidation();

            var result = productValidation.Validate(product);

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == nameof(Product.Name) &&
                                               e.ErrorMessage == "�������� ������ ����������� ��� ����������.");
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void DescriptionIsEmptyTheory_Failer(string description)
        {
            var product = new ProductDto
            {
                Name = "name",
                Description = description,
                Price = 10.99m,
                ProductCategoryId = 1
            };
            var productValidation = new ProductValidation();

            var result = productValidation.Validate(product);

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == nameof(Product.Description) &&
                                               e.ErrorMessage == "�������� ������ ����������� ��� ����������.");
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void CheckCorrectPrice_Correct(decimal price)
        {
            var product = new ProductDto
            {
                Name = "name",
                Description = "description",
                Price = price,
                ProductCategoryId = 1
            };
            var productValidation = new ProductValidation();

            var result = productValidation.Validate(product);

            Assert.True(result.IsValid);
        }

        [Theory]
        [InlineData(-10)]
        [InlineData(-15)]
        [InlineData(-25)]
        public void CheckFailerPrice_Failer(decimal price)
        {
            var product = new ProductDto
            {
                Name = "name",
                Description = "description",
                Price = price,
                ProductCategoryId = 1
            };
            var productValidation = new ProductValidation();

            var result = productValidation.Validate(product);

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == nameof(Product.Price) &&
                                               e.ErrorMessage == "���� ������ ������ ���� ������ ����.");
        }

    }
}