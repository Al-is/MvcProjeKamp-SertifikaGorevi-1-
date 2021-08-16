using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;

namespace BusinessLayer.Concrete
{
    public class ContentManager : IContentService
    {
        private IContentDal _contentDal;

        public ContentManager(IContentDal contentDal)
        {
            _contentDal = contentDal;
        }

        public List<Content> GetList(string content)
        {
            if (string.IsNullOrEmpty(content))
            {
                return _contentDal.List();
            }
            else
            {
                return _contentDal.List(x => x.ContentValue.Contains(content));
            }
        }

        public void ContentAdd(Content content)
        {
            _contentDal.Insert(content);
        }

        public Content GetById(int id)
        {
            return _contentDal.Get(x => x.ContentId == id);
        }

        public void ContentDelete(Content content)
        {
            _contentDal.Delete(content);
        }

        public void ContentUpdate(Content content)
        {
            _contentDal.Update(content);
        }

        public List<Content> GetListByHeadingId(int id)
        {
            return _contentDal.List(x => x.HeadingId == id);
        }

        public List<Content> GetListByWriter(int id)
        {
            return _contentDal.List(x => x.WriterId == id);
        }
    }
}
