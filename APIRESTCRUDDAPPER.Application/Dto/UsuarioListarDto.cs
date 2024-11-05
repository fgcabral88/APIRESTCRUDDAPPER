﻿using APIRESTCRUDDAPPER.Domain.Enums;

namespace APIRESTCRUDDAPPER.Dto
{
    public class UsuarioListarDto
    {
        public int Id { get; set; }
        public string NomeCompleto { get; set; }
        public string Email { get; set; }
        public string Cargo { get; set; }
        public double Salario { get; set; }
        public SituacaoType Situacao { get; set; } 
    }
}