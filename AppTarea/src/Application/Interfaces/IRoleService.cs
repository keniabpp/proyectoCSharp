namespace Application.Interfaces
{
    /// <summary>
    /// Interfaz para obtener información de roles desde Identity
    /// </summary>
    public interface IRoleService
    {
        Task<string?> GetRoleNameByIdAsync(int roleId);
    }
}
