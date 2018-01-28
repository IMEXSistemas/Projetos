using System;
using System.Collections.Generic;
using System.Text;

namespace BMSworks.Model
{
    [Serializable]
    public enum Operadores
    {
        Equals,
        NotEquals,
        Like,
        NotLike,
        GreaterThan,
        GreaterOrEquals,
        LessThan,
        LessOrEquals,
        Blank
    }

    [Serializable]
    public enum Condicional
    {
        AND,
        OR
    }        

    [Serializable]
    public class WhereClause
    {
        private int idClausula;
        private string campo;
        private Type tipo;
        private Operadores operador;
        private string valor;
        private Condicional condicional;

        public WhereClause(string Campo, Type Tipo, Operadores Operador, string Valor, Condicional condicional) 
        {
            this.campo = Campo;
            this.tipo = Tipo;
            this.operador = Operador;
            this.valor = Valor;
            this.condicional = condicional;
        }

        public WhereClause() { }

        public int IdClausula
        {
            get { return idClausula; }
            set { idClausula = value; }
        }
        public string Campo
        {
            get { return campo; }
            set { campo = value; }
        }
        public Type Tipo
        {
            get { return tipo; }
            set { tipo = value; }
        }
        public Operadores Operador
        {
            get { return operador; }
            set { operador = value; }
        }
        public string Valor
        {
            get { return valor; }
            set { valor = value; }
        }
        public Condicional Condicional
        {
            get { return condicional; }
            set { condicional = value; }
        }
    }
}
