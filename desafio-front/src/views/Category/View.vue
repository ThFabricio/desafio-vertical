<template>
    <div class="container my-5">
      <div class="card shadow-sm">
        <div class="card-header d-flex flex-column flex-md-row justify-content-between align-items-center">
          <h3 class="mb-3 mb-md-0">Categorias</h3>
          <RouterLink to="/categories/new" class="btn btn-primary">
            Criar nova categoria
          </RouterLink>
        </div>
        <div class="card-body p-0">
          <div class="table-responsive">
            <table class="table table-striped mb-0">
              <thead class="table-dark">
                <tr>
                  <th>Título</th>
                  <th>Descrição</th>
                  <th>Código</th>
                  <th>Ações</th>
                </tr>
              </thead>
              <tbody>
                <tr v-for="categoria in categorias" :key="categoria.id">
                  <td>{{ categoria.title }}</td>
                  <td>{{ categoria.description }}</td>
                  <td>{{ categoria.code }}</td>
                  <td>
                    <RouterLink
                      :to="`/categories/${categoria.id}/edit`"
                      class="btn btn-sm btn-warning me-2"
                    >
                      Editar
                    </RouterLink>
                    <button
                      @click="deletarCategoria(categoria.id)"
                      class="btn btn-sm btn-danger"
                    >
                      Deletar
                    </button>
                  </td>
                </tr>
              </tbody>
            </table>
          </div>
        </div>
      </div>
    </div>
  </template>
  
<script>
    import axios from 'axios'

    export default {
        data() {
            return {
            categorias: []
            }
        },
        mounted() {
            this.fetchCategorias()
        },
        methods: {
            fetchCategorias() {
                axios
                    .get('http://localhost:8080/api/v1/category')
                    .then(response => {
                    console.log(response)
                    this.categorias = response.data.categories
                    })
                    .catch(error => console.error(error))
            },
            deletarCategoria(id) {
                if (confirm('Deseja realmente deletar esta categoria?')) {
                    axios
                    .delete(`http://localhost:8080/api/v1/category/${id}`)
                    .then(() => this.fetchCategorias())
                    .catch(error => console.error(error))
                }
            }
        }
    }
</script>
  
<style scoped>
.table-responsive {
border-top: 1px solid #dee2e6;
}
</style>
  