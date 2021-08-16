using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityLayer.Concrete;

namespace BusinessLayer.Abstract
{
    public interface IMessageService
    {
        List<Message> GetListInbox(string adminMail);
        List<Message> GetListSendbox(string adminMail);
        List<Message> ReadList();
        List<Message> UnReadList();
        List<Message> DraftList();
        void MessageAdd(Message message);
        Message GetById(int id);
        void MessageDelete(Message message);
        void AddDraft(Message message);
        void ReadStatus(Message message);
    }
}
