using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUD_Biblioteca.Models
{
    public class AutorMap : ClassMap<Autor>
    {
        public AutorMap(){
            Id(c => c.Id);
            Map(c => c.Nome);
            Table("Autores");
        }
    }
}
