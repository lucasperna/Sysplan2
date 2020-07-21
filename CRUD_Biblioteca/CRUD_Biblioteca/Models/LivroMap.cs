using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUD_Biblioteca.Models
{
    public class LivroMap : ClassMap<Livro>
    {
        public LivroMap()
        {
            Id(c => c.Id);
            References(c => c.Autor);
            Map(c => c.QtdeEstoque);
            Table("Livros");
        }
    }
}
