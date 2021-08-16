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
    public class MessageManager : IMessageService
    {
        private IMessageDal _messageDal;

        public MessageManager(IMessageDal messageDal)
        {
            _messageDal = messageDal;
        }

        public Message GetById(int id)
        {
            return _messageDal.Get(x => x.MessageId == id);
        }

        public List<Message> GetListInbox(string adminMail)
        {
            return _messageDal.List(x => x.ReceiverMail == adminMail);
        }

        public List<Message> GetListSendbox(string adminMail)
        {
            return _messageDal.List(x => x.SenderMail == adminMail);
        }

        public void MessageAdd(Message message)
        {
            _messageDal.Insert(message);
        }

        public void MessageDelete(Message message)
        {
            throw new NotImplementedException();
        }

        public void AddDraft(Message message)
        {
            if (message.Draft == false)
            {
                message.Draft = true;
            }
            _messageDal.Insert(message);
        }
        public List<Message> DraftList()
        {
            return _messageDal.List(x => x.Draft == true);
        }
        public void ReadStatus(Message message)
        {
            if (message.Read == false)
            {
                message.Read = true;
            }
            _messageDal.Update(message);
        }
        public List<Message> ReadList()
        {
            return _messageDal.List(x => x.Read == true);
        }
        public List<Message> UnReadList()
        {
            return _messageDal.List(x => x.Read == false);
        }
    }
}

