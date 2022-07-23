import './App.css';
import 'bootstrap/dist/css/bootstrap.min.css';
import {BrowserRouter} from 'react-router-dom';
import Routing from './Component/Routing';
import SignUp from './Component/Modal/SignUp';
import Login from './Component/Modal/Login';
function App () {
  return (
    <BrowserRouter>
      <Routing />
      <SignUp />
      <Login />
    </BrowserRouter>
  );
}
export default App;
