using API_ANSPRICING.Models;
using eTagTech.SDK.Core;
using eTagTech.SDK.Core.Entity;
using eTagTech.SDK.Core.Enum;
using eTagTech.SDK.Core.Event;
using log4net.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace API_ANSPRICING.Services
{
    public class StationManager
    {
        private readonly ILogger<StationManager> logger;
        public StationManager(ILogger<StationManager> logger)
        {
            this.logger = logger;
        }
        public Result EditTag(Guid TagId)
        {
            Tag tag = new Tag();
            using(DatabaseContext db = new DatabaseContext())
            {
                tag = db.tags.AsNoTracking().Where(x => x.id == TagId).Include(x=>x.station).FirstOrDefault();
            }

            if(tag == null)
            {
                throw new NullReferenceException();
            }

            logger.LogInformation("Tag: " + tag.ToString());

            Bitmap img = null;

            switch (tag.type)
            {
                case ESLType.ESL154:
                    img = CreateBMPForESL154(tag);
                    break;
                case ESLType.ESL213:
                    img = CreateBMPForESL213(tag);
                    break;
                case ESLType.ESL290:
                    img = CreateBMPForESL290(tag);
                    break;
                case ESLType.ESL420:
                    img = CreateBMPForESL420(tag);
                    break;
                case ESLType.ESL750:
                    img = CreateBMPForESL750(tag);
                    break;
                default:
                    break;
            }

            return Send(img, tag);
        }

        private Bitmap CreateBMPForESL154(Tag tag)
        {
            // Шрифты, тебе нужно будет менять только размер шрифта
            var fontName = new Font("Arial Black", 22, FontStyle.Regular, GraphicsUnit.Pixel);
            var fontCountry = new Font("Tahoma", 20, FontStyle.Regular, GraphicsUnit.Pixel);
            var fontOldPrice = new Font("Tahoma", 20, FontStyle.Strikeout, GraphicsUnit.Pixel);
            var fontPrice = new Font("Arial Black", 32, FontStyle.Regular, GraphicsUnit.Pixel);
            var fontDescription = new Font("Tahoma", 16, FontStyle.Regular, GraphicsUnit.Pixel);

            // Размер ценника (хз откуда взял)
            int width = 154;
            int height = 154;

            Bitmap image = new Bitmap(width, height);
            var graphics = Graphics.FromImage(image);

            graphics.FillRectangle(Brushes.Red, 0, 0, width, 32);

            // Название товара по центру
            int center = (width / 2) - (tag.name.Length / 2);
            graphics.DrawString(tag.name, fontName, Brushes.White, new Point(36, 0));

            graphics.DrawString(tag.coutry, fontCountry, Brushes.Black, new Point(80, 40));
            graphics.DrawString(tag.oldPrice, fontOldPrice, Brushes.Black, new Point(80, 65));
            graphics.DrawString(tag.price, fontPrice, Brushes.Red, new Point(40, 85));

            graphics.DrawString(tag.description1, fontDescription, Brushes.Black, new Point(3, 25));
            graphics.DrawString(tag.description2, fontDescription, Brushes.Black, new Point(3, 40));
            graphics.DrawString(tag.description3, fontDescription, Brushes.Black, new Point(3, 55));
            graphics.DrawString(tag.description4, fontDescription, Brushes.Black, new Point(3, 70));
            graphics.DrawString(tag.description5, fontDescription, Brushes.Black, new Point(3, 85));

            return image;
        }

        private Bitmap CreateBMPForESL213(Tag tag)
        {
            // Шрифты, тебе нужно будет менять только размер шрифта
            var fontName = new Font("Arial Black", 16, FontStyle.Regular, GraphicsUnit.Pixel);
            var fontCountry = new Font("Tahoma", 14, FontStyle.Regular, GraphicsUnit.Pixel);
            var fontOldPrice = new Font("Tahoma", 14, FontStyle.Strikeout, GraphicsUnit.Pixel);
            var fontPrice = new Font("Arial Black", 28, FontStyle.Regular, GraphicsUnit.Pixel);
            var fontDescription = new Font("Tahoma", 14, FontStyle.Regular, GraphicsUnit.Pixel);

            // Размер ценника (хз откуда взял)
            int width = 213;
            int height = 200;

            Bitmap image = new Bitmap(width, height);
            var graphics = Graphics.FromImage(image);

            graphics.FillRectangle(Brushes.Red, 0, 0, width, 20);

            // Название товара по центру
            int center = (width / 2) - (tag.name.Length / 2);
            graphics.DrawString(tag.name, fontName, Brushes.White, new Point(70, 0));

            graphics.DrawString(tag.coutry, fontCountry, Brushes.Black, new Point(135, 25));
            graphics.DrawString(tag.oldPrice, fontOldPrice, Brushes.Black, new Point(160, 45));
            graphics.DrawString(tag.price, fontPrice, Brushes.Red, new Point(120, 65));

            graphics.DrawString(tag.description1, fontDescription, Brushes.Black, new Point(3, 25));
            graphics.DrawString(tag.description2, fontDescription, Brushes.Black, new Point(3, 40));
            graphics.DrawString(tag.description3, fontDescription, Brushes.Black, new Point(3, 55));
            graphics.DrawString(tag.description4, fontDescription, Brushes.Black, new Point(3, 70));
            graphics.DrawString(tag.description5, fontDescription, Brushes.Black, new Point(3, 85));

            return image;
        }

        private Bitmap CreateBMPForESL290(Tag tag)
        {
            // Шрифты, тебе нужно будет менять только размер шрифта
            var fontName = new Font("Arial Black", 25, FontStyle.Regular, GraphicsUnit.Pixel);
            var fontCountry = new Font("Tahoma", 16, FontStyle.Regular, GraphicsUnit.Pixel);
            var fontOldPrice = new Font("Tahoma", 16, FontStyle.Strikeout, GraphicsUnit.Pixel);
            var fontPrice = new Font("Arial Black", 30, FontStyle.Regular, GraphicsUnit.Pixel);
            var fontDescription = new Font("Tahoma", 16, FontStyle.Regular, GraphicsUnit.Pixel);

            // Размер ценника (хз откуда взял)
            int width = 294;
            int height = 213;

            Bitmap image = new Bitmap(width, height);
            var graphics = Graphics.FromImage(image);

            graphics.FillRectangle(Brushes.Red, 0, 0, width, 30);

            // Название товара по центру
            int center = (width / 2) - (tag.name.Length / 2);
            graphics.DrawString(tag.name, fontName, Brushes.White, new Point(75, 0));

            graphics.DrawString(tag.coutry, fontCountry, Brushes.Black, new Point(220, 35));
            graphics.DrawString(tag.oldPrice, fontOldPrice, Brushes.Black, new Point(240, 65));
            graphics.DrawString(tag.price, fontPrice, Brushes.Red, new Point(180, 85));

            graphics.DrawString(tag.description1, fontDescription, Brushes.Black, new Point(3, 35));
            graphics.DrawString(tag.description2, fontDescription, Brushes.Black, new Point(3, 50));
            graphics.DrawString(tag.description3, fontDescription, Brushes.Black, new Point(3, 65));
            graphics.DrawString(tag.description4, fontDescription, Brushes.Black, new Point(3, 80));
            graphics.DrawString(tag.description5, fontDescription, Brushes.Black, new Point(3, 95));

            return image;

        }

        private Bitmap CreateBMPForESL420(Tag tag)
        {
            // Шрифты, тебе нужно будет менять только размер шрифта
            var fontName = new Font("Arial Black", 25, FontStyle.Regular, GraphicsUnit.Pixel);
            var fontCountry = new Font("Tahoma", 18, FontStyle.Regular, GraphicsUnit.Pixel);
            var fontOldPrice = new Font("Tahoma", 18, FontStyle.Strikeout, GraphicsUnit.Pixel);
            var fontPrice = new Font("Arial Black", 34, FontStyle.Regular, GraphicsUnit.Pixel);
            var fontDescription = new Font("Tahoma", 14, FontStyle.Regular, GraphicsUnit.Pixel);

            // Размер ценника (хз откуда взял)
            int width = 420;
            int height = 420;

            Bitmap image = new Bitmap(width, height);
            var graphics = Graphics.FromImage(image);

            graphics.FillRectangle(Brushes.Red, 0, 0, width, 40);

            // Название товара по центру
            int center = (width / 2) - (tag.name.Length / 2);
            graphics.DrawString(tag.name, fontName, Brushes.White, new Point(75, 0));

            graphics.DrawString(tag.coutry, fontCountry, Brushes.Black, new Point(3, 55));
            graphics.DrawString(tag.manufacturer, fontDescription, Brushes.Black, new Point(3, 75));
            graphics.DrawString(tag.oldPrice, fontOldPrice, Brushes.Black, new Point(150, 60));
            graphics.DrawString(tag.price, fontPrice, Brushes.Red, new Point(80, 75));

            graphics.DrawString(tag.description1, fontDescription, Brushes.Black, new Point(3, 140));
            graphics.DrawString(tag.description2, fontDescription, Brushes.Black, new Point(3, 160));
            graphics.DrawString(tag.description3, fontDescription, Brushes.Black, new Point(3, 180));
            graphics.DrawString(tag.description4, fontDescription, Brushes.Black, new Point(3, 200));
            graphics.DrawString(tag.description5, fontDescription, Brushes.Black, new Point(3, 220));
            graphics.DrawString(tag.description6, fontDescription, Brushes.Black, new Point(3, 240));
            graphics.DrawString(tag.imgSource, fontDescription, Brushes.Black, new Point(300, 50));
            graphics.DrawImage(Image.FromFile("screwdriver.png"), 230, 60, 150, 150);
            return image;

        }

        private Bitmap CreateBMPForESL750(Tag tag)
        {
            // Шрифты, тебе нужно будет менять только размер шрифта
            var fontName = new Font("Arial Black", 32, FontStyle.Regular, GraphicsUnit.Pixel);
            var fontCountry = new Font("Tahoma", 24, FontStyle.Regular, GraphicsUnit.Pixel);
            var fontOldPrice = new Font("Tahoma", 26, FontStyle.Strikeout, GraphicsUnit.Pixel);
            var fontPrice = new Font("Arial Black", 32, FontStyle.Regular, GraphicsUnit.Pixel);
            var fontDescription = new Font("Tahoma", 18, FontStyle.Regular, GraphicsUnit.Pixel);

            // Размер ценника (хз откуда взял)
            int width = 750;
            int height = 600;

            Bitmap image = new Bitmap(width, height);
            var graphics = Graphics.FromImage(image);

            graphics.FillRectangle(Brushes.Red, 0, 0, width, 55);

            // Название товара по центру
            int center = (width / 2) - (tag.name.Length / 2);
            graphics.DrawString(tag.name, fontName, Brushes.White, new Point(75, 0));

            graphics.DrawString(tag.coutry, fontCountry, Brushes.Black, new Point(3, 70));
            graphics.DrawString(tag.manufacturer, fontDescription, Brushes.Black, new Point(3, 100));
            graphics.DrawString(tag.oldPrice, fontOldPrice, Brushes.Black, new Point(280, 70));
            graphics.DrawString(tag.price, fontPrice, Brushes.Red, new Point(225, 105));

            graphics.DrawString(tag.description1, fontDescription, Brushes.Black, new Point(3, 180));
            graphics.DrawString(tag.description2, fontDescription, Brushes.Black, new Point(3, 200));
            graphics.DrawString(tag.description3, fontDescription, Brushes.Black, new Point(3, 220));
            graphics.DrawString(tag.description4, fontDescription, Brushes.Black, new Point(3, 240));
            graphics.DrawString(tag.description5, fontDescription, Brushes.Black, new Point(3, 260));
            graphics.DrawString(tag.description6, fontDescription, Brushes.Black, new Point(3, 280));
            graphics.DrawString(tag.imgSource, fontDescription, Brushes.Black, new Point(300, 50));
            graphics.DrawImage(Image.FromFile("screwdriver.png"), 400, 70, 220, 220);
            return image;


        }

        private Result Send(Bitmap image, Tag tag)
        {
            TagEntity t0 = new TagEntity
            {
                R = false,                          // LED red turn off
                B = false,                          // LED blue turn off
                G = false,                           // LED green turn on
                Times = 0,                         // LED light flashing 50 times
                Before = false,                     // LED light flashing after screen refresh
                TagType = tag.type,          // The tag type is ESL290R
                PageIndex = PageIndex.P0,           // Refresh the 1st page
                Pattern = Pattern.UpdateDisplay,    // Update data cache and refresh screen
                StationID = tag.station.stationID,                   // Station ID is 01
                Status = TagStatus.Unknow,          // Default tag status is Unknow
                TagID = tag.tagId,                 // Tag ID
                ServiceCode = new Random(DateTime.Now.Millisecond).Next(65536),   // Token, between 0~65535
                DataList = new List<DataEntity>     // Data List
                {
                    new ImageEntity
                    {
                        ImageType = ImageType.Image,
                        Data = image,
                        W = 1,
                        H = 1,
                        ID = 1,
                        Color = FontColor.Red
                    }
                }
            };

            Server.Instance.StationEventHandler += Instance_StationEventHandler;
            Server.Instance.ResultEventHandler += Instance_ResultEventHandler;

            Server.Instance.Start(1234);
            Result r0 = Server.Instance.Send(tag.station.shopCode, tag.station.stationID, t0, true, true);


            logger.LogInformation("Tag " + tag.tagId + " Station " + tag.station.Name);
            logger.LogInformation(JsonConvert.SerializeObject(tag));
            logger.LogInformation(r0.ToString());

            return r0;
        }




        /// <summary>
        /// Instance of result event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void Instance_ResultEventHandler(object sender, ResultEventArgs e)
        {
            Console.WriteLine("Shop Code:{0}, AP:{1}, Result Type:{2}, Count:{3}", e.ShopCode, e.StationID, e.ResultList, e.ResultList.Count);
            foreach (var item in e.ResultList)
            {
                Console.WriteLine(" >> Tag ID:{0}, Status:{1}, Temperature:{2}, Power:{3}, Signal:{4}, Key: {5},Token:{6}",
                    item.TagID, item.TagStatus, item.Temperature, item.PowerValue, item.Signal, item.ResultType, item.Token);
            }
        }

        /// <summary>
        /// Instance of station event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void Instance_StationEventHandler(object sender, StationEventArgs e)
        {
            Console.WriteLine("Shop Code:{0} AP: {1} IP:{2} Online:{3}", e.Shop, e.ID, e.IP, e.Online);
        }
    }
}
