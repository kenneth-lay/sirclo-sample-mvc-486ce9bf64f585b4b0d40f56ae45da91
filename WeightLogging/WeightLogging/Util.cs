using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WeightLogging.Models;

namespace WeightLogging
{
    public class Util
    {
        public bool SaveChangesToDB(Controller c, weightlogEntities db, object[] records, bool isEdit = false)
        {
            try
            {
                foreach (object record in records)
                {
                    Type recordType = record.GetType();

                    if (isEdit)
                    {
                        db.Entry(record).State = EntityState.Modified;
                    }

                    DbSet dbSet = db.Set(record.GetType());
                    if (!isEdit) dbSet.Add(record);
                }

                db.SaveChanges();
                c.TempData["RecordSaved"] = "Success";
                return true;
            }
            catch (Exception e)
            {
                c.TempData["RecordSaved"] = "Failed";
                return false;
            }
        }
    }
}