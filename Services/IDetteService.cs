using gestion_dette_web.core.service;
using gestion_dette_web.Models;

namespace gestion_dette_web.services;

public interface IDetteService : IService<Dette>
{
    List<Dette> GetDettes(int page, int pageSize);
    int GetTotalDettes();
}