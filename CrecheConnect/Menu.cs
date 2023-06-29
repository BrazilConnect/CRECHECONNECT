using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace CrecheConnect
{
    internal class Menu
    {
        internal int _CurrentID = 1;
        internal List<Aluno> _Alunos = new List<Aluno>();

        internal void ChamarMenu()
        {
            Console.WriteLine($">>>>> MENU  ALUNOS <<<<<");
            Console.WriteLine("========================");
            Console.WriteLine("Escolha a opção desejada");
            Console.WriteLine("========================");
            Console.WriteLine("A - Adicionar");
            Console.WriteLine("B - Exibir dados");
            Console.WriteLine("C - Alterar dados");
            Console.WriteLine("D - Excluir dados");
            Console.WriteLine("========================");
            Console.Write(">>> Informe sua escolha: ");

            switch(Console.ReadLine().ToString().ToUpper())
            {
                case "A":
                    Console.WriteLine(">>> ADICIONAR <<<");
                    Aluno aluno = new Aluno(true);
                    aluno.Save();
                    Console.WriteLine("Aluno adicionado com sucesso!");
                    Thread.Sleep(1000);
                    Console.Clear();
                    ChamarMenu();
                    break;
                case "B":
                    Console.WriteLine(">>> EXIBIR DADOS <<<");
                    Console.Write("Informe o ID do aluno ou 'todos' para exibir todos os cadastros: ");
                    var escolha = Console.ReadLine().ToUpper();
                    if (escolha == "TODOS")
                    {
                        var alunoo = new Aluno(false);

                        foreach (var alunooo in alunoo.Read())
                        {
                            Console.WriteLine($"ID Aluno: ({alunooo.Id}). \n\tCPF: {alunooo.Cpf}. \n\tNome completo: {alunooo.NomeCompleto}. \n\tData de nascimento: {alunooo.DataNascimento.ToString("dd/MM/yyyy")}. \n\tGênero: {alunooo.Genero}.");
                        }
                    }
                    else 
                    {
                        if (long.TryParse(escolha, out var idReader))
                        {
                            var alunoReader = new Aluno(idReader);

                            if (alunoReader.IsValid())
                            {

                                alunoReader.Read();
                                Console.WriteLine($"ID Aluno: ({alunoReader.Id}). \n\tCPF: {alunoReader.Cpf}. \n\tNome completo: {alunoReader.NomeCompleto}. \n\tData de nascimento: {alunoReader.DataNascimento.ToString("dd/MM/yyyy")}. \n\tGênero: {alunoReader.Genero}.");
                            }
                            else
                            {
                                Console.WriteLine($"Não existe aluno com o ID {idReader}.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Opção inválida!");
                        }
                    }
                    Console.ReadKey();
                    Thread.Sleep(1000);
                    Console.Clear();
                    ChamarMenu();
                    break;
                case "C":
                    Console.WriteLine(">>> ALTERAR DADOS <<<");

                    Console.Write("Informe o ID do aluno: ");
                    var idUpdate = long.Parse(Console.ReadLine());
                    var alunoUpdate = new Aluno(idUpdate);

                    if (alunoUpdate.IsValid())
                    {
                        AlterarAluno(alunoUpdate);
                    }
                    else
                    {
                        Console.WriteLine($"Não existe aluno com o ID {idUpdate}.");
                    }

                    Thread.Sleep(1000);
                    Console.Clear();
                    ChamarMenu();
                    break;
                case "D":
                    Console.WriteLine(">>> EXCLUIR DADOS <<<");
                    Console.Write("Informe o ID do aluno: ");
                    var idDelete = long.Parse(Console.ReadLine());
                    var alunoDelete = new Aluno(idDelete);

                    if(alunoDelete.IsValid())
                    {
                        alunoDelete.Delete();
                        Console.WriteLine("Dados excluídos com sucesso.");
                    }
                    else
                    {
                        Console.WriteLine($"Não existe aluno com o ID {idDelete}.");
                    }

                    Thread.Sleep(1000);
                    Console.Clear();
                    ChamarMenu();
                    break;
                default:
                    Console.WriteLine("Opção inválida!");

                    Thread.Sleep(1000);
                    Console.Clear();
                    ChamarMenu();
                    break;
            }

        }

        internal void AlterarAluno(Aluno aluno)
        {
            Console.Clear();
            Console.WriteLine($">>> MENU  ALTERAÇÃO <<<");
            Console.WriteLine("========================");
            Console.WriteLine("Escolha a opção desejada");
            Console.WriteLine("========================");
            Console.WriteLine("A - CPF");
            Console.WriteLine("B - Nome completo");
            Console.WriteLine("C - Data de nascimento");
            Console.WriteLine("D - Gênero");
            Console.WriteLine("========================");
            Console.Write(">>> Informe sua escolha, digite 'salvar' para salvar as alterações: ");
            var escolha = Console.ReadLine().ToUpper();
            while (escolha != "SALVAR")
            {
                if (escolha != "A" && escolha != "B" && escolha != "C" && escolha != "D")
                {
                    Console.WriteLine("Opção inválida!");
                    break;
                }
                else
                {
                    switch(escolha)
                    {
                        case "A":
                            Console.Write("Informe o CPF correto: ");
                            var cpfCorreto = Console.ReadLine();
                            aluno.Cpf = cpfCorreto;
                            break;
                        case "B":
                            Console.Write("Informe o nome completo correto: ");
                            var nomeCorreto = Console.ReadLine();
                            aluno.NomeCompleto = nomeCorreto;
                            break;
                        case "C":
                            Console.Write("Informe a data de nascimento correta [DD/MM/AAAA]: ");

                            if (DateTime.TryParse(Console.ReadLine(), out DateTime dataCorreta))
                            {
                                aluno.DataNascimento = dataCorreta;
                                Thread.Sleep(1000);
                            }
                            else
                            {
                                Console.Write("Opção inválida!");
                            }
                            break;
                        case "D":
                            Console.Write("Informe o gênero correto [F/M]: ");
                            var generoCorreto = Console.ReadLine();
                            aluno.Genero = (Enumeradores.Genero)Enum.Parse(typeof(Enumeradores.Genero), generoCorreto);
                            break;

                    }

                    Console.Clear();
                    Console.WriteLine($">>> MENU ALTERAÇÃO <<<");
                    Console.WriteLine("========================");
                    Console.WriteLine("Escolha a opção desejada");
                    Console.WriteLine("========================");
                    Console.WriteLine("A - CPF");
                    Console.WriteLine("B - Nome completo");
                    Console.WriteLine("C - Data de nascimento");
                    Console.WriteLine("D - Gênero");
                    Console.WriteLine("========================");
                    Console.Write(">>> Informe sua escolha, digite 'salvar' para salvar as alterações: ");
                    escolha = Console.ReadLine().ToUpper();
                }

                aluno.Update();
                Console.WriteLine("Dados alterados com sucesso.");
            }

        }
        private void ListarAlunos ()
        {

            if (_Alunos.Count < 1)
            {
                Console.WriteLine("Sem dados para exibir.");
            }
            else
            {
                foreach (var aluno in _Alunos)
                {
                    Console.WriteLine($"ID Aluno: ({aluno.Id}). \n\tCPF: {aluno.Cpf}. \n\tNome completo: {aluno.NomeCompleto}. \n\tData de nascimento: {aluno.DataNascimento.ToString("dd/mm/yyyy")}. \n\tGênero: {aluno.Genero}.");
                }
            }
        }
    }
}
