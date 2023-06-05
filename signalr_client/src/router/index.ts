import { createRouter, createWebHistory, RouteRecordRaw } from 'vue-router'
import HomeView from '../views/HomeView.vue'
import ProgressView from '../views/ProgressView.vue'
import SignalRTest from '../views/SignalRTest.vue'
import SignalRTest2Parent from '../views/SignalRTest2Parent.vue'

const routes: Array<RouteRecordRaw> = [
  {
    path: '/',
    name: 'home',
    component: HomeView
  },
  {
    path: '/about',
    name: 'about',
    // route level code-splitting
    // this generates a separate chunk (about.[hash].js) for this route
    // which is lazy-loaded when the route is visited.
    component: () => import(/* webpackChunkName: "about" */ '../views/AboutView.vue')
  },
  {
    path: '/Progress',
    name: 'Progress',
    component: ProgressView
  },
  {
    path: '/SignalRTest',
    name: 'SignalRTest',
    component: SignalRTest
  },
  {
    path: '/SignalRTest2Parent',
    name: 'SignalRTest2Parent',
    component: SignalRTest2Parent
  }
]

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes
})

export default router
