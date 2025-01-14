<template>
    <div class="container mt-5">
      <div class="card shadow-sm">
        <div class="card-header d-flex flex-column flex-md-row justify-content-between align-items-center">
          <h3 class="mb-0">Adicionar Categoria</h3>
          <RouterLink to="/" class="btn btn-primary">
            Voltar
          </RouterLink>
        </div>
        <div class="card-body">
          <form @submit.prevent="submitForm">
            <div class="mb-3">
              <label for="title" class="form-label">Título</label>
              <input
                type="text"
                id="title"
                v-model="category.title"
                class="form-control"
                placeholder="Digite o título"
                required
              />
            </div>
            <div class="mb-3">
              <label for="description" class="form-label">Descrição</label>
              <textarea
                id="description"
                v-model="category.description"
                class="form-control"
                rows="3"
                placeholder="Digite a descrição"
                required
              ></textarea>
            </div>
            <div class="mb-3">
              <label for="code" class="form-label">Código</label>
              <input
                type="text"
                id="code"
                v-model="category.code"
                class="form-control"
                placeholder="Digite o código"
                required
              />
            </div>
            <button type="submit" class="btn btn-success">
              Criar Categoria
            </button>
          </form>
        </div>
      </div>
    </div>
</template>
  
<script>
    import { RouterLink } from 'vue-router'
    import axios from 'axios'

    export default {
        components: { RouterLink },
        data() {
            return {
            category: {
                title: '',
                description: '',
                code: ''
            }
            }
        },
        methods: {
            submitForm() {
            axios.post('http://localhost:8080/api/v1/category', this.category)
                .then(response => {
                    if (response.status === 201) {
                        alert('Categoria criada com sucesso!')
                    }
                    this.$router.push('/')
                    
                })
                .catch(error => {
                    if (error.response.status === 409) {
                        if(error.response.data.message === 'Title is already in use') 
                        {
                            alert('O título informado já está em uso.')
                        }
                        else if(error.response.data.message === 'Code is already in use')
                        {
                            alert('O código informado já está em uso.')
                        }
                        else
                        {
                            alert('Erro ao criar categoria.')
                        }
                    }
                })
            }
        }
    }
</script>
  
<style scoped>
    .container {
    max-width: 600px;
    }
</style>
  