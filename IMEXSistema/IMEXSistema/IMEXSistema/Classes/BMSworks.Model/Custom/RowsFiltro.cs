using System;
using System.Text;

namespace BMSworks.Model
{
    /// <summary>
    /// Classe de transporte para modelar a entidade de filtro utilizada 
    /// pelo userControl ListaFiltros.
    /// </summary>
    [Serializable]
    public class RowsFiltro
    {
        private string campo;
        private string tipo;
        private string operador;
        private string valor;
        private string condicao;

        public RowsFiltro() { }

        /// <summary>
        /// Passa por default a condição de "AND"
        /// </summary>
        /// <param name="Campo">Nome do campo na tabela.</param>
        /// <param name="Tipo">Tipo do campo no .Net ex: System.Int32</param>
        /// <param name="Operador">Operador para consulta, ex: =, &#60;, &#62;</param>
        /// <param name="Valor">Valor para comparação</param>
        public RowsFiltro(string Campo, string Tipo, string Operador, string Valor)
        {
            this.campo = Campo;
            this.tipo = Tipo;
            this.operador = Operador;
            this.valor = Valor;
            this.condicao = "AND";
        }

        public RowsFiltro(string Campo, string Tipo, string Operador, string Valor, string Condicao)
        {
            this.campo = Campo;
            this.tipo = Tipo;
            this.operador = Operador;
            this.valor = Valor;
            this.condicao = Condicao;
        }

        public string Campo
        {
            get { return campo; }
            set { campo = value; }
        }

        public string Tipo
        {
            get { return tipo; }
            set { tipo = value; }
        }

        public string Operador
        {
            get { return operador; }
            set { operador = value; }
        }

        public string Valor
        {
            get { return valor; }
            set { valor = value; }
        }

        public string Condicao
        {
            get { return condicao; }
            set { condicao = value; }
        }
    }
}