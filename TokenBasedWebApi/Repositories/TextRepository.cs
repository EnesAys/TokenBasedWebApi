using LengerProje.DATA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LengerProje.Models;
using LengerProje.Helpers;
using System.Reflection;
using LengerProje.Helpers.Enums;

namespace LengerProje.Repositories
{
    public class TextRepository
    {
        private ModelEntities db;
        public TextRepository()
        {
            db = new ModelEntities();
        }
        public IEnumerable<Texts> GetTexts()
        {
            // Yayında olan bütün yazıları getirir
            var Texts = db.Texts.Where(x => x.IsRelease == true).ToList();
            return Texts;
        }
        public bool AddText(Text modeltext)
        {
            var text = new Texts();
            try
            {
                text.Title = modeltext.Title;
                text.Content = modeltext.Content;
                text.TextTypeID = modeltext.TextTypeID;
                text.UserID = modeltext.UserID;
                text.IsRelease = modeltext.IsRelease==null ? false:modeltext.IsRelease.Value;
                text.Release_date = DateTime.Now;
                db.Texts.Add(text);
                db.SaveChanges();
                Logger.SendLog("TextRepository/AddText", modeltext, false, LogTypes.Response);
                return true;
            }
            catch (Exception ex)
            {
                Logger.SendLog("TextRepository/AddText", modeltext+" "+ex.ToString(), true, LogTypes.Response);
                return false;
            }
        }
        public bool DeleteText(int textID)
        {
            try
            {
                var text = db.Texts.FirstOrDefault(x=>x.ID==textID);
                db.Texts.Remove(text);
                db.SaveChanges();
                Logger.SendLog("TextRepository/DeleteText", textID , false, LogTypes.Response);
                return true;

            }
            catch (Exception ex) 
            {
                Logger.SendLog("TextRepository/DeleteText", textID+" "+ex.ToString(), true, LogTypes.Response);
                return false;
            }


        }

    }
}