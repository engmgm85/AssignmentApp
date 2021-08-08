import React from 'react';
import 'office-ui-fabric-react/dist/css/fabric.css';
import './App.css';
import Navigation from './Components/Navigation';
import GridTable from './Components/GridTable'
import { QueryClient, QueryClientProvider } from "react-query";
import { Switch, Route, BrowserRouter as Router } from "react-router-dom";
import { Survey } from './pages/Survey';
import {SurveyList} from './pages/SurveyList'

export const App: React.FunctionComponent = () => {
  const queryClient = new QueryClient();
  function AppRouter() {
    return (
      <div id="wrapper">
          <Router>
        <Switch>
        <Route path="/Survey">
              <Survey />
            </Route>
        <Route path="/">
              <SurveyList />
            </Route>
            
           
          </Switch>
          </Router>
        </div>
     ) }
  return (
    <QueryClientProvider client={queryClient}>
      
   <div className="ms-Grid" dir="ltr">
    <div className="ms-Grid-row">
      <div className="ms-Grid-col ms-sm2 ms-xl2">
        <Navigation />
      </div>
      <div className="main-element ms-Grid-col ms-sm10 ms-xl10">
        <div className="ms-Grid-row">
        <AppRouter />
        </div>
     
      </div>
    </div>
  </div> 
  </QueryClientProvider>
  );
};
