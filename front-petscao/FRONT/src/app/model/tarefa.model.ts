import { Categoria } from "./categoria.model";

export interface Tarefa {
  tarefaId?: number;
  Titulo?: string;
  Descricao?: string;
  criadoEm?: string;
  categoriaId?: number;
}
