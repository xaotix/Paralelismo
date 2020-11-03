using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Paralelismo
{
    class Program
    {
        static void Main(string[] args)
        {
            DateTime Inicio = DateTime.Now;
            int loops = 20;
            Proximo();

            //loop em um laço normal
            Inicio = DateTime.Now;
            Console.WriteLine(@"Começou em : {0}", Inicio);
            for (int i = 0; i < loops; i++)
            {
                long total = RodarSubTarefa();
                Console.WriteLine("{0} - {1}", i, total);
            }
            Console.WriteLine(@"Terminou em : {0}", DateTime.Now);
            Console.WriteLine(@"Tempo para rodar usando Laço: {0}", (DateTime.Now - Inicio).TotalMilliseconds);
            Proximo();


            //loop em um laço usando paralelismo
            Inicio = DateTime.Now;
            Console.WriteLine(@"Começou em : {0}", Inicio);
            Parallel.For(0, loops, i =>
            {
                long total = RodarSubTarefa();
                Console.WriteLine("{0} - {1}", i, total);
            });
            Console.WriteLine(@"Terminou em : {0}", DateTime.Now);
            Console.WriteLine(@"Tempo para rodar usando Laço Parallel: {0}", (DateTime.Now - Inicio).TotalMilliseconds);
            Proximo();


            //loop em um laço usando paralelismo, definindo o número máximo de threads para 2
            Inicio = DateTime.Now;
            Console.WriteLine(@"Começou em : {0}", Inicio);
            Parallel.For(0, loops, new ParallelOptions() { MaxDegreeOfParallelism = 2 }, i =>
            {
                long total = RodarSubTarefa();
                Console.WriteLine("{0} - {1}", i, total);
            });
            Console.WriteLine(@"Terminou em : {0}", DateTime.Now);
            Console.WriteLine(@"Tempo para rodar usando Laço Parallel com 2 threads simultâneos: {0}", (DateTime.Now - Inicio).TotalMilliseconds);
            Proximo();

            //laço usando task factory
            List<Task> Tarefas = new List<Task>();
            Inicio = DateTime.Now;
            Console.WriteLine(@"Começou em : {0}", Inicio);
            for (int i = 0; i < loops; i++)
            {
                Tarefas.Add(Task.Factory.StartNew(() =>
                {
                    long total = RodarSubTarefa();
                    Console.WriteLine("{0} - {1}", i, total);
                }));

            }
            Task.WaitAll(Tarefas.ToArray());
            Console.WriteLine(@"Terminou em : {0}", DateTime.Now);
            Console.WriteLine(@"Tempo para rodar usando Laço com Task Factory: {0}", (DateTime.Now - Inicio).TotalMilliseconds);
            Proximo();
        }

        private static void Proximo()
        {
            Console.WriteLine("".PadLeft(45, '-'));
            Console.WriteLine("Aperte qualquer tecla para continuar.");
            Console.ReadLine();
        }

        static long RodarSubTarefa()
        {
            //um pequeno loop, apenas pra demonstrar uma sub-tarefa rodando
            long total = 0;
            for (int i = 1; i < 100000000; i++)
            {
                total += i;
            }
            return total;
        }
    }
}
