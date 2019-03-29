using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Calculadora.Controllers {
   public class HomeController : Controller {

      // GET: Home
      public ActionResult Index() {

         // definir o valor inicial das variáveis
         Session["LimpaEcra"] = true;
         Session["Operador"] = "";

         return View();
      }


      // POST: Home
      [HttpPost]
      public ActionResult Index(string visor, string bt) {

         switch(bt) {
            // selecionei um algarismo
            case "0":
            case "1":
            case "2":
            case "3":
            case "4":
            case "5":
            case "6":
            case "7":
            case "8":
            case "9":
               if(visor == "0" || (bool)Session["LimpaEcra"]) { // visor.Equals("0")
                  visor = bt;
                  // marca que o VISOR não deve ser limpo
                  Session["LimpaEcra"] = false;
               }
               else {
                  visor += bt;
               }
               break;

            // selecionei o '+/-'

            // selecionei o ','

            // selecionei um símbolo de operação: +, -, x, :
            case "+":
            case "-":
            case "x":
            case ":":
            case "=":

               // já se pressionou, alguma vez, um sinal de operador?
               if((string)Session["Operador"] == "") {
                  // é a 1ªx que se carregou num operador
                  // guardar operador
                  Session["Operador"] = bt;
                  // guardar o primeiro operando
                  Session["PrimeiroOperando"] = visor;
                  // marcar a calculadora (leia-se, o VISOR) para ser reiniciada
                  Session["LimpaEcra"] = true;
               }
               else{
                  // já há 2 operandos e 1 operador
                  // já se pode executar a operação aritmética

                  // recuperar os dados da operação aritmérica
                  double operando1 = Convert.ToDouble(Session["PrimeiroOperando"]);
                  double operando2 = Convert.ToDouble( visor);
                  string operador = (string)Session["Operador"];

                  // agora já posso fazer a operação
                  switch(operador){
                     case "+":
                        visor = operando1 + operando2 +"";
                        break;
                     case "-":
                        visor = operando1 - operando2 + "";
                        break;
                     case "x":
                        visor = operando1 * operando2 + "";
                        break;
                     case ":":
                        visor = operando1 / operando2 + "";
                        break;
                  } // fim da operação aritmética

                  // preparar a calculadora para continuar as operações
                  // guardar operador
                  Session["Operador"] = bt;
                  // guardar o primeiro operando
                  Session["PrimeiroOperando"] = visor;
                  // marcar a calculadora (leia-se, o VISOR) para ser reiniciada
                  Session["LimpaEcra"] = true;
               } // fim do IF

               break;



         }






         ViewBag.Resposta = visor;

         return View();
      }



   }
}