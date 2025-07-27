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
                Name = "Игровая гарнитура Razer Kraken X",
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
                                               e.ErrorMessage == "Название товара обязательно для заполнения.");
        }

        [Fact]
        public void NameExceedsMaxLenght_Failer()
        {
            var product = new ProductDto
            {
                Name = "Ноутбук ASUS ZenBook 14 Монитор Dell UltraSharp 27 Клавиатура Logitech G Pro Мышь Razer DeathAdder V2 Внешний жесткий диск Seagate 2TB Гарнитура HyperX Cloud II" +
                "Веб-камера Logitech C920 Картридж HP 302 Сетевой фильтр APC Кабель HDMI 2.0 Планшет Microsoft Surface Pro 7 Игровая клавиатура Razer BlackWidow Мышь Logitech MX " +
                "Master 3 Внешний SSD Samsung T7 1TB Монитор LG 34UM69G-B Игровая гарнитура SteelSeries Arctis 7 Принтер Canon PIXMA TS8320 Веб-камера AVerMedia Live Streamer Камера " +
                "GoPro HERO9 Беспроводные наушники Apple AirPods Pro Микрофон Blue Yeti USB Внешний DVD-привод ASUS USB 3.0 Клавиатура Corsair K95 RGB Мышь SteelSeries Rival 600 " +
                "Игровой монитор AOC 24G2 144Hz Внешний жесткий диск WD My Passport 4TB Сетевой адаптер TP-Link Archer T3U Беспроводная клавиатура Logitech K400 Plus Игровая мышь " +
                "HyperX Pulsefire FPS, Сетевой фильтр Defender 8 розеток, Клавиатура Logitech K120, Мышь A4Tech Bloody V8, Внешний SSD Crucial X8 1TB, Монитор Samsung Odyssey G5, " +
                "Веб-камера Microsoft LifeCam HD-3000 Игровая гарнитура Razer Kraken X Мышь Zowie FK1 Игровой монитор MSI Optix MAG241C Клавиатура HyperX Alloy FPS Pro",
                Description = "Valid Description",
                Price = 10.99m,
                ProductCategoryId = 1
            };
            var productValidation = new ProductValidation();

            var result = productValidation.Validate(product);

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == nameof(Product.Name) &&
                                               e.ErrorMessage == $"Название товара не должно превышать {ProductValidation.MaxNameLength} символов.");
        }

        [Fact]
        public void CorrectValidationProductDescriiption_Correct()
        {
            var product = new ProductDto
            {
                Name = "Игровая гарнитура Razer Kraken X",
                Description = "Игровая гарнитура Razer Kraken X — это легкая и удобная гарнитура, разработанная для длительных игровых сессий. Она оснащена 40-мм драйверами," +
                "обеспечивающими четкий и объемный звук, что позволяет лучше слышать детали в играх. Микрофон с шумоподавлением гарантирует чистую передачу голоса, а гибкая" +
                "конструкция позволяет легко регулировать положение. Гарнитура совместима с различными платформами, включая ПК, консоли и мобильные устройства. Благодаря мягким" +
                "амбушюрам и регулируемому оголовью, Razer Kraken X обеспечивает комфорт даже во время самых напряженных игр.",
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
                                               e.ErrorMessage == "Описание товара обязательно для заполнения.");
        }

        [Fact]
        public void DescriptionExceedsMaxLenght_Failer()
        {
            var product = new ProductDto
            {
                Name = "XYZ Pro 12",
                Description = "Смартфон XYZ Pro 12 представляет собой флагманское устройство, которое сочетает в себе передовые технологии и стильный дизайн, что делает его идеальным " +
                "выбором для пользователей, стремящихся к качеству и функциональности. Он оснащен 6.7-дюймовым AMOLED дисплеем с разрешением 3200 x 1440 пикселей, который обеспечивает " +
                "яркие и насыщенные цвета, а также глубокий черный цвет благодаря поддержке HDR10+. Дисплей защищен стеклом Gorilla Glass Victus, что гарантирует его долговечность и " +
                "устойчивость к царапинам. На задней панели расположена тройная камера, состоящая из основной камеры на 108 МП с оптической стабилизацией, ультраширокой камеры " +
                "на 12 МП с углом обзора 120 градусов и телефото-камеры на 12 МП с 3-кратным оптическим зумом, что позволяет делать качественные снимки в любых условиях. Фронтальная " +
                "камера на 32 МП поддерживает режимы портретной съемки и запись видео в формате 4K, что делает ее отличным выбором для селфи и видеозвонков. Внутри устройства " +
                "установлен мощный процессор XYZ A15, который обеспечивает высокую производительность и энергоэффективность. В сочетании с 12 ГБ оперативной и 256 ГБ встроенной памяти " +
                "пользователи могут без проблем запускать ресурсоемкие приложения и игры. Поддержка 5G позволяет наслаждаться молниеносной скоростью интернета, что особенно актуально " +
                "для стриминга и онлайн-игр. Аккумулятор емкостью 5000 мАч обеспечивает длительное время работы без подзарядки, а поддержка быстрой зарядки мощностью 65 Вт позволяет " +
                "зарядить устройство до 100% всего за 30 минут. Также доступна функция беспроводной зарядки и обратной беспроводной зарядки, что позволяет заряжать другие устройства, " +
                "такие как наушники или смарт-часы. Смартфон предлагает великолепное звучание благодаря стереодинамикам и поддержке Dolby Atmos, что делает его отличным выбором для " +
                "любителей музыки и фильмов. Он работает на операционной системе XYZ OS 12, основанной на Android, с множеством предустановленных приложений и возможностью настройки " +
                "интерфейса под себя. Дополнительные функции включают сканер отпечатков пальцев, встроенный в дисплей, поддержку распознавания лиц для дополнительной безопасности, а " +
                "также защиту от воды и пыли по стандарту IP68, что делает его идеальным для активного образа жизни. Смартфон также поддерживает все современные стандарты подключения, " +
                "включая Wi-Fi 6, Bluetooth 5.2 и NFC, что позволяет легко подключать устройства, такие как наушники, умные часы и другие гаджеты. XYZ Pro 12 — это не просто телефон, " +
                "а мощный инструмент, который поможет вам оставаться на связи, создавать и делиться контентом, а также наслаждаться мультимедийными развлечениями.",
                Price = 10.99m,
                ProductCategoryId = 1
            };
            var productValidation = new ProductValidation();

            var result = productValidation.Validate(product);

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == nameof(Product.Description) &&
                                               e.ErrorMessage == $"Описание товара не должно превышать {ProductValidation.MaxDescriptionLength} символов.");
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
                                                e.ErrorMessage == "Цена товара должна быть больше нуля.");
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
                                               e.ErrorMessage == "Название товара обязательно для заполнения.");
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
                                               e.ErrorMessage == "Описание товара обязательно для заполнения.");
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
                                               e.ErrorMessage == "Цена товара должна быть больше нуля.");
        }

    }
}