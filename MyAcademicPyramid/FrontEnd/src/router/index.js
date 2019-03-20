import Vue from 'vue'
import Hello from '@/components/HelloWorld'
import Router from 'vue-router'
import Login from '@/components/Login'
import UserList from '@/components/UserList'
import UserForm from '@/components/UserInfoForm'

Vue.use(Router)
export const bus = new Vue();
export default new Router({
  mode: 'history',
  base: process.env.BASE_URL,
  routes: [
	{
		path: '/',
		name: 'Hello',
		component: Hello
	},
    {
      path: '/login',
      name: 'Login',
      component: Login
    },
    {
      path: '/UserManagement',
      name: 'UserManagement',
      component: UserList, UserForm
    }
  ]
})