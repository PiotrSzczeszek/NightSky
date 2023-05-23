import './app.css'
import App from './App.svelte'

// src/i18n.js
import { addMessages, init, getLocaleFromNavigator } from 'svelte-i18n';

import en from '../locales/en.json';
import pl from '../locales/pl.json';

addMessages('en', en);
addMessages('pl', pl);

init({
  fallbackLocale: 'en',
  initialLocale: "pl",
});

const app = new App({
  target: document.getElementById('app'),
})

export default app
