using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoApi.Model
{
    public class TodoItem
    {
        public TodoItem()
        {
        }

        public TodoItem(int id, string nome, bool finalizado)
        {
            Id = id;
            Nome = nome;
            Finalizado = finalizado;
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public bool Finalizado { get; set; }
    }
}