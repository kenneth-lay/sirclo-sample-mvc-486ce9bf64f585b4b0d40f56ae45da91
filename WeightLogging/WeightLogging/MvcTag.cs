using System;
using System.Web.Mvc;

namespace WeightLogging
{
    public class MvcTag : IDisposable
    {
        private readonly ViewContext viewContext;
        private bool disposed;
        private string endTag;

        public MvcTag(ViewContext viewContext, string startTag, string endTag)
        {
            if (viewContext == null)
            {
                throw new ArgumentNullException();
            }

            this.viewContext = viewContext;
            this.endTag = endTag;

            this.viewContext.Writer.Write(startTag);
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                disposed = true;

                viewContext.Writer.Write(endTag);
                viewContext.OutputClientValidation();
            }
        }

        public void EndOfTag()
        {
            Dispose(true);
        }
    }
}