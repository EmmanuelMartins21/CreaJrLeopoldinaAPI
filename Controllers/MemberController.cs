using CreaJrLeopoldinaAPI.Context;
using CreaJrLeopoldinaAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using System.Net;
using System.Xml.Linq;

namespace CreaJrLeopoldinaAPI.Controllers
{
    [Route("api/crea")]
    [ApiController]
    public class MemberController : ControllerBase
    {
        private ILogger<MemberController> _logger;
        private readonly CreaJrLeopoldinaAPIContext _context;

        public MemberController(ILogger<MemberController> logger, CreaJrLeopoldinaAPIContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet("getallmembers")]
        public ActionResult<IEnumerable<Member>> GetAllMembers()
        {
            try
            {
                var allMembers = _context.Members.ToList();

                if (allMembers.Count == 0) return NotFound("Não foram encontrados membros");

                return Ok(allMembers);
            }
            catch (DbException ex)
            {
                return StatusCode(500, "Erro interno do servidor ao acessar o banco de dados: " + ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Falha ao buscar membros ativos" + ex.Message);
                return StatusCode(500, "Erro interno do servidor.");
            }
            finally
            {
                _context.SaveChanges();
                _context.Dispose();
            }
        }

        [HttpGet("getenabledmembers")]
        public IActionResult GetEnabledMembers()
        {
            try
            {
                var enabledMembers = _context.Members
                    .Where(em => em.Enable)
                    .ToList();

                if (enabledMembers.Count == 0)
                {
                    return NotFound("Não foram encontrados membros habilitados.");
                }
                return Ok(enabledMembers);
            }
            catch (DbException ex)
            {
                return StatusCode(500, "Erro interno do servidor ao acessar o banco de dados: " + ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Falha ao buscar membros ativos" + ex.Message);
                return StatusCode(500, "Erro interno do servidor.");
            }
            finally
            {
                _context.Dispose();
            }
        }


        [HttpGet("getbirthdaypersons")]
        public ActionResult<IEnumerable<Member>> GetBirthdayPersonMembers()
        {
            var memberEmpty = new Member();

            try
            {
                var members = _context.Members.
                    Where(em => em.Birth.Month == DateTime.Now.Month).ToList();

                if (members.Count == 0) return NotFound(memberEmpty.ToString()); 

                return Ok(members);
            }
            catch (DbException ex)
            {
                return NotFound("Houve um erro ao buscar registros no banco: " + ex.Message);
            }
        }

        //Adicionar autenticação 
        /*
        [HttpPost("postmember")]
        public ActionResult PostMember(Member member)
        {
            try
            {
                var allMembers = _context.Members.Where(m=> m.Enable == true).ToList();

                if (allMembers.Contains(member))
                {
                    return NotFound($"Membro já cadastrado");
                }
                else
                {
                    _context.Members.Add(member);
                    _context.SaveChanges();
                }
                return CreatedAtAction(nameof(GetAllMembers), new {Name = member.Name}, member);
            }
            catch (DbException ex)
            {
                return StatusCode(500, "Erro interno do servidor ao acessar o banco de dados: " + ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Falha ao registrar membros " + ex.Message);
                return StatusCode(500, "Erro interno do servidor.");
            }
            finally
            {
                _context.SaveChanges();
                _context.Dispose();
            }
        }



        [HttpPost("postseveralmembers")]
        public ActionResult<IEnumerable<Member>> PostMember(List<Member> members)
        {
            try
            {
                if (members.Count > 0)
                {
                    var allMember = _context.Members.ToList();
                    if (allMember.Count != 0)
                    {
                        foreach (var m in members)
                        {
                            bool memberExist = allMember.Any(members => members == m);

                            if (!memberExist)
                            {
                                _context.Members.Add(m);
                                _context.SaveChanges();
                            }
                        }
                    }
                    else
                    {
                        foreach (var a in members)
                        {
                            _context.Members.Add(a);
                            _context.SaveChanges();
                        }
                    }
                    _logger.LogInformation($"POST /Membros cadastrados com Sucesso");
                    _context.SaveChanges();
                }

                return CreatedAtAction(nameof(GetAllMembers), new { members }, members);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocorreu um erro ao adicionar um membro");
                return StatusCode(500);
            }
        }*/

    }
}
