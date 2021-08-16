using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityLayer.Concrete;

namespace BusinessLayer.Abstract
{
    public interface IContentService
    {
        List<Content> GetList(string content);
        List<Content> GetListByWriter(int id);
        List<Content> GetListByHeadingId(int id);
        void ContentAdd(Content content);

        Content GetById(int id);

        void ContentDelete(Content content);
        void ContentUpdate(Content content);
    }
}
