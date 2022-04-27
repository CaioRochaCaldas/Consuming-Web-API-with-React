
import './App.css';
import 'bootstrap/dist/css/bootstrap.min.css';
import axios from 'axios';
import {Modal,ModalBody,ModalFooter,ModalHeader} from 'reactstrap';
import logoCadastro from './assets/cadastro.png';
import { useEffect,useState } from 'react';

function App() {

const baseUrl ="https://localhost:7243/api/alunos";

const [data,setData] = useState([]);
const [modalIncluir,setModalInluir] = useState(false);

const [alunoSelecionado,setAlunoSelecionado] = useState({
  id: '',
  name: '',
  email: '',
  idade: ''
})

const abrirFecharModalIncluir=()=>{
  setModalInluir(!modalIncluir);
}

//metodo handle changes permite o crud na aplicação
const handleChange = e => {
  const {name,value} = e.target;
  setAlunoSelecionado({
    ...alunoSelecionado,[name]:value
  });
  console.log(alunoSelecionado);
}

// pedidos get de todos os alunos
const pedidoGet = async()=>{
  await axios.get(baseUrl).then(response => { 
    setData(response.data);
  }).catch(error=>{
    console.log(error);
  })
}

// buscar um aluno por id 
const pedidoPost=async()=> {
  delete alunoSelecionado.id;
  alunoSelecionado.idade=parseInt(alunoSelecionado.idade);
    await axios.post(baseUrl,alunoSelecionado)
    .then(response=>{
      setData(data.concat(response.data));
      abrirFecharModalIncluir();
    }).catch(error => {
      console.log(error);
    })
}

useEffect(()=>{
  pedidoGet();
})


  return (
    <div className="aluno-container">
     <br/>
     <h3>Cadastro de Alunos</h3>
     <header>
       <img src={logoCadastro} alt='Cadastro'/>
        <button className='btn btn-success' onClick={()=> abrirFecharModalIncluir()}>Incluir Novo Aluno</button>
     </header>
      <table className='table table-bordered'>
        
        <thead>
          <tr>
            <th>Id</th>
            <th>Nome</th>
            <th>Email</th>
            <th>Idade</th>
            <th>Operação</th>
          </tr>
        </thead>
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
      
      <Modal isOpen={modalIncluir}>
        <ModalHeader>Incluir Alunos</ModalHeader>
        <ModalBody>
          <div className="form-group">
            <label>Nome: </label>
            <br />
            <input type="text" className="form-control" name="name" onChange={handleChange} />
            <br />
            <label>Email: </label>
            <br />
            <input type="text" className="form-control" name="email" onChange={handleChange} />
            <br />
            <label>Idade: </label>
            <br />
            <input type="text" className="form-control" name="idade" onChange={handleChange} />
            <br />
          </div>
        </ModalBody>
        <ModalFooter>
          <button className='btn btn-primary' onClick={()=> pedidoPost()}>Incluir</button>{" "}
          <button className='btn btn-danger' onClick={()=>abrirFecharModalIncluir()}>Cancelar</button>
        </ModalFooter>
      </Modal>

    </div>
  );
}

export default App;
