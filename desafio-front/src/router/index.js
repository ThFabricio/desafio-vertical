import { createRouter, createWebHistory } from 'vue-router'
import CategoryView from '../views/Category/View.vue'
import CategoryCreate from '../views/Category/Create.vue'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'home',
      component: CategoryView,
    },
    {
      path: '/categories/new',
      name: 'categoryCreate',
      component: CategoryCreate,
    },
    {
      path: '/about',
      name: 'about',
      // route level code-splitting
      // this generates a separate chunk (About.[hash].js) for this route
      // which is lazy-loaded when the route is visited.
      component: () => import('../views/AboutView.vue'),
    },
  ],
})

export default router
