<template>
    <div class="container mt-5">
      <div class="card shadow-sm">
        <div class="card-header d-flex flex-column flex-md-row justify-content-between align-items-center">
          <h3 class="mb-0">Editar Categoria</h3>
          <RouterLink to="/" class="btn btn-primary">Voltar</RouterLink>
        </div>
        <div class="card-body">
          <form @submit.prevent="submitForm" novalidate>
            <div class="mb-3">
              <label for="title" class="form-label">Título</label>
              <input
                type="text"
                id="title"
                v-model="category.title"
                class="form-control"
                placeholder="Digite o título"
                required
                maxlength="100"
              />
              <span class="text-danger" v-if="errors.title">{{ errors.title }}</span>
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
                maxlength="255"
              ></textarea>
              <span class="text-danger" v-if="errors.description">{{ errors.description }}</span>
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
                pattern="^[A-Za-z0-9]*$"
              />
              <span class="text-danger" v-if="errors.code">{{ errors.code }}</span>
            </div>
  
            <button type="submit" class="btn btn-success">Salvar Alterações</button>
          </form>
        </div>
      </div>
    </div>
</template>
    
<script>
    import { RouterLink } from 'vue-router'
    import axios from 'axios'
    import Swal from 'sweetalert2'  
    
    export default {
        components: { RouterLink },
        data() {
            return {
            category: {
                title: '',
                description: '',
                code: ''
            },
            errors: {} 
            }
        },
        mounted(){
            this.getCategoryData(this.$route.params.id);
        },
        methods: {
            getCategoryData(id) {
                axios.get(`http://localhost:8080/api/v1/category/${id}`)
                .then(response => {
                    this.category.title = response.data.category.title;
                    this.category.description = response.data.category.description;
                    this.category.code = response.data.category.code;
                })
                .catch(error => {
                    Swal.fire({
                    title: 'Erro',
                    text: 'Ocorreu um erro inesperado.',
                    icon: 'error',
                    confirmButtonText: 'OK'
                    })
                })
            },
            submitForm() {
                this.errors = {}

                if (!this.category.title) {
                    this.errors.title = "O título é obrigatório.";
                } else if (this.category.title.length > 100) {
                    this.errors.title = "O título deve ter no máximo 100 caracteres.";
                }

                if (!this.category.description) {
                    this.errors.description = "A descrição é obrigatória.";
                } else if (this.category.description.length > 255) {
                    this.errors.description = "A descrição deve ter no máximo 255 caracteres.";
                }

                if (!this.category.code) {
                    this.errors.code = "O código é obrigatório.";
                } else if (!/^[A-Za-z0-9]*$/.test(this.category.code)) {
                    this.errors.code = "O código deve conter apenas letras e números.";
                }

                if (Object.keys(this.errors).length > 0) {
                return;
                }

                axios.put('http://localhost:8080/api/v1/category/' + this.$route.params.id, this.category)
                .then(response => {
                    if (response.status === 200) {
                        Swal.fire({
                        title: 'Sucesso!',
                        text: 'Categoria editada com sucesso!',
                        icon: 'success',
                        confirmButtonText: 'OK'
                        })
                    }
                    this.$router.push('/')
                })
                .catch(error => {
                if (error.response && error.response.status === 409) {
                    let message = 'Erro ao criar categoria.'
                    if (error.response.data.message === 'Title is already in use') {
                        message = 'O título informado já está em uso.'
                    } else if (error.response.data.message === 'Code is already in use') {
                        message = 'O código informado já está em uso.'
                    }
                    Swal.fire({
                        title: 'Erro',
                        text: message,
                        icon: 'error',
                        confirmButtonText: 'OK'
                    })
                } else {
                    Swal.fire({
                        title: 'Erro',
                        text: 'Ocorreu um erro inesperado.',
                        icon: 'error',
                        confirmButtonText: 'OK'
                    })
                }
                })
            },
        }
   }
</script>
    
<style scoped>
.container {
  max-width: 600px;
}
.text-danger {
  font-size: 0.875em;
  margin-top: 0.25rem;
  color: #dc3545;
}
</style>
    