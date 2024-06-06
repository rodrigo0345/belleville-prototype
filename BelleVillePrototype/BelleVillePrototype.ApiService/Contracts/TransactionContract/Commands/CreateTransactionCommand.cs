using System.ComponentModel.DataAnnotations;
using BelleVillePrototype.ApiService.Entities;

namespace BelleVillePrototype.ApiService.Contracts.TransactionContract.Commands;

public class CreateTransactionCommand
{
    public bool IsActive { get; set; } = true;
    public DateTime Data { get; set; }
    
    [Required(ErrorMessage = "O Tipo de Transação é obrigatório")]
    public TransactionType Tipo { get; set; }
    
    [Required(ErrorMessage = "A Transação tem de estar associada a uma Chave")]
    public Guid ChaveId { get; set; }
    
    [Required(ErrorMessage = "A Transação tem de estar associada a um Utilizador")]
    public Guid? UserId { get; set; }
    
    // apenas deve ser utilizado quando a transacao está em tipo externo
    [Phone(ErrorMessage= "O Telefone tem de ser válido")]
    public string? Phone { get; set; } = string.Empty;
}