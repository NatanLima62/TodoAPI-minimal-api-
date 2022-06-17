using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoApi.Model
{
    public class Todo
    {
        public Todo()
        {
        }

        public Todo(int id, string nome, string descricao, DateTime dataCriacao, bool finalizado)
        {
            Id = id;
            Nome = nome;
            Descricao = descricao;
            DataCriacao = DateTime.Now;
            Finalizado = false;
            TodoItems = new List<TodoItem>();
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public DateTime DataCriacao { get; set; }
        public bool Finalizado { get; set; }
        public List<TodoItem> TodoItems { get; set; }
    }
}