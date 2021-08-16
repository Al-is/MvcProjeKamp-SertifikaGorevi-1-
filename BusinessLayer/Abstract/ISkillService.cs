using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityLayer.Concrete;

namespace BusinessLayer.Abstract
{
    public interface ISkillService
    {
        List<Skill> GetList();
        void SkillAdd(Skill skill);
        void SkillUpdate(Skill skill);
        Skill GetById(int id);
    }
}
