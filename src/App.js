
import './App.css';
import 'bootstrap/dist/css/bootstrap.min.css';
import axios from 'axios';
import {Modal,ModalBody,ModalFooter,ModalHeader} from 'reactstrap';
import logoCadastro from './assets/cadastro.png';

function App() {

const baseUrl ="https://localhost:7243/api/alunos";

  return (
    <div className="App">
     <br/>
     <h3>Cadastro de Alunos</h3>
     <header>
       <img src={logoCadastro} alt='Cadastro'/>
        <button className='btn btn-success'>Incluir Novo Aluno</button>
     </header>
      <table className='table table-bordered'>
        <thread>
          <tr>
            <th>Id</th>
            <th>Nome</th>
            <th>Email</th>
            <th>Idade</th>
            <th>Operação</th>
          </tr>
        </thread>
        <tbody>

        </tbody>
      </table>
    </div>
  );
}

export default App;
