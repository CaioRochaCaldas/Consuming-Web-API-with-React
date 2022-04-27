
import './App.css';
import 'bootstrap/dist/css/bootstrap.min.css';
import axios from 'axios';
import {Modal,ModalBody,ModalFooter,ModalHeader} from 'reactstrap';
import logoCadastro from './assets/cadastro.png';
import { useEffect,useState } from 'react';

function App() {

const baseUrl ="https://localhost:7243/api/alunos";

const [data,setData] = useState([]);

const pedidoGet = async()=>{
  await axios.get(baseUrl).then(response => { //requisição get ao endereço definido
    setData(response.data);
  }).catch(error=>{
    console.log(error);
  })
}

useEffect(()=>{
  pedidoGet();
})


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

          {data.map(aluno=>( //mapeie o usestate dos dados do Get e mostre o id,nome,email,idade lá da api
            <tr key={aluno.id}>
              <td>{aluno.id}</td>
              <td>{aluno.name}</td>
              <td>{aluno.email}</td>
              <td>{aluno.idade}</td>
              <td>
                <button className='btn btn-primary'>Editar</button>
                <button className='btn btn-danger'>Excluir</button>
              </td>
            </tr>
          ))}

        </tbody>
      </table>
    </div>
  );
}

export default App;
