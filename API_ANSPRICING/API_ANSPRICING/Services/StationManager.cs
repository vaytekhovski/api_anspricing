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
        public Tag EditTag(Tag tag)
        {
            logger.LogInformation("Entred tag with name " + tag.name);
            var img = CreateBMP(tag);
            Send(img, tag);

            return tag;
        }


        private Bitmap CreateBMP(Tag tag)
        {
            var fontName = new Font("Arial Black", 16, FontStyle.Regular, GraphicsUnit.Pixel);
            var fontCountry = new Font("Tahoma", 14, FontStyle.Regular, GraphicsUnit.Pixel);
            var fontOldPrice = new Font("Tahoma", 14, FontStyle.Strikeout, GraphicsUnit.Pixel);
            var fontPrice = new Font("Arial Black", 28, FontStyle.Regular, GraphicsUnit.Pixel);
            var fontDescription = new Font("Tahoma", 14, FontStyle.Regular, GraphicsUnit.Pixel);

            Bitmap image = new Bitmap(213, 200);
            var graphics = Graphics.FromImage(image);

            graphics.FillRectangle(Brushes.Red, 0, 0, 213, 20);

            graphics.DrawString(tag.name, fontName, Brushes.White, new Point(70, 0));

            graphics.DrawString(tag.coutry, fontCountry, Brushes.Black, new Point(145, 25));
            graphics.DrawString(tag.oldPrice, fontOldPrice, Brushes.Black, new Point(160, 45));
            graphics.DrawString(tag.price, fontPrice, Brushes.Red, new Point(120, 65));

            graphics.DrawString(tag.description1, fontDescription, Brushes.Black, new Point(3, 25));
            graphics.DrawString(tag.description2, fontDescription, Brushes.Black, new Point(3, 40));
            graphics.DrawString(tag.description3, fontDescription, Brushes.Black, new Point(3, 55));
            graphics.DrawString(tag.description4, fontDescription, Brushes.Black, new Point(3, 70));
            graphics.DrawString(tag.description5, fontDescription, Brushes.Black, new Point(3, 85));

            return image;

        }

        private void Send(Bitmap image, Tag tag)
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
                    },

                    new BarcodeEntity
                    {
                        BarcodeType = BarcodeType.Code128,
                        Color = FontColor.Black,
                        Data = tag.QrCode,
                        Height = 5,
                        ID = 3,
                        InvertColor = false,
                        W = 70,
                        H = 70,
                    },
                }
            };

            Result r0 = Server.Instance.Send(tag.station.shopCode, tag.station.stationID, t0, true, true);


            logger.LogInformation("Tag " + tag.tagId + " Station " + tag.station.Name);
            logger.LogInformation(JsonConvert.SerializeObject(tag));
            logger.LogInformation(r0.ToString());

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
