import * as React from 'react';
import * as ReactDOM from 'react-dom';

import { Router, Route, Switch, BrowserRouter } from 'react-router-dom';
import { HomeComponent } from './components/HomeComponent';
import AboutComponent from './components/AboutComponent';


ReactDOM.render(
  <BrowserRouter>
    <main>
      <header>
        Header
      </header>

      <Switch>
        {/* use exact to show default root url */}
        <Route exact path="/" component={HomeComponent} />
        <Route path="/About" component={AboutComponent} />
        <Route path="*" component={() => <div>Not found 404</div>} />
      </Switch>
    </main>

  </BrowserRouter>,
  document.getElementById('root')
);