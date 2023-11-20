import { HttpClient } from "@angular/common/http";
import { Component, OnInit } from "@angular/core";
import {Router} from "@angular/router";
import { Tarefa } from "../../model/tarefa.model";

@Component({
  selector: "app-listar-tarefa",
  templateUrl: "./listar-tarefa.component.html",
  styleUrls: ["./listar-tarefa.component.css"],
})
export class ListarTarefaComponent implements OnInit{
  constructor(private http: HttpClient) {}
  columnsTable: string [] = [
    "Titulo",
    "Descricao",
  ]

 tarefas : Tarefa[] = [];


public ngOnInit(): void {
  this.http
    .get<Tarefa[]>("https://localhost:5001/api/tarefa/listar")
    .subscribe({
      //Requisição com sucesso
      next: (tarefas) => {
        console.table(tarefas);
        this.tarefas = tarefas;
      },
      //Requisição com erro
      error: (erro) => {
        console.log(erro);
        console.log('Erro ao obter dados do servidor');
        
      },
    });
}


}
