
import './App.css';
import 'bootstrap/dist/css/bootstrap.min.css';
import axios from 'axios';
import {Modal,ModalBody,ModalFooter,ModalHeader} from 'reactstrap';
import logoCadastro from './assets/cadastro.png';
import { useEffect,useState } from 'react';

function App() {

const baseUrl ="https://localhost:7243/api/alunos";

const [data,setData] = useState([]);
const [modalIncluir,setModalInluir] = useState(false); //add aluno
const [modalEditar,setModalEditar] = useState(false); //editar aluno
const [modalExcluir,setModalExcluir] = useState(false);//modal de excluir aluno
const [updateData,setUpdateData] = useState(true);
  
//metodo para capturar dados do formulario e salvar
const [alunoSelecionado,setAlunoSelecionado] = useState({
  id: '',
  name: '',
  email: '',
  idade: ''
})



//metodo editar aluno do modal editar aluno ou excluir aluno
const selecionarAluno = (aluno,opcao) =>{
  setAlunoSelecionado(aluno);
  (opcao === "Editar") ? //operador de escolha
  abrirFecharModalEditar(): abrirFecharModalExcluir();
}

//modal add aluno
const abrirFecharModalIncluir=()=>{
  setModalInluir(!modalIncluir);
}

//modal editar aluno
const abrirFecharModalEditar=()=>{
  setModalEditar(!modalEditar);
}
//modal excluir aluno
const abrirFecharModalExcluir=()=>{
  setModalExcluir(!modalExcluir);
}


//metodo handle changes permite o crud na aplicação do formulario
const handleChange = e => {
  const {name,value} = e.target;
  setAlunoSelecionado({
    ...alunoSelecionado,[name]:value
  });
  console.log(alunoSelecionado);
}

// busca todos os alunos
const pedidoGet = async()=>{
  await axios.get(baseUrl).then(response => { 
    setData(response.data);
  }).catch(error=>{
    console.log(error);
  })
}

// buscar cria aluno 
const pedidoPost=async()=> {
  delete alunoSelecionado.id; //temos que deletar esse id do aluno pois quem vai lidar com isso é o DB da API e não oFrontend
  alunoSelecionado.idade=parseInt(alunoSelecionado.idade);
    await axios.post(baseUrl,alunoSelecionado)
    .then(response=>{
      setData(data.concat(response.data));
      setUpdateData(true);
      abrirFecharModalIncluir();
    }).catch(error => {
      console.log(error);
    })
}

//alterar aluno selecionado por id
const pedidoPut = async()=>{
  alunoSelecionado.idade=parseInt(alunoSelecionado.idade);
  await axios.put(baseUrl+"/"+alunoSelecionado.id,alunoSelecionado)
  .then(response=>{
    var resposta = resposta.data;
    var dadosAuxiliar=data;
    dadosAuxiliar.map(aluno=>{
      if(aluno.id===alunoSelecionado.id){
        aluno.id=resposta.name;
        aluno.email=resposta.email;
        aluno.idade=resposta.idade;
      }
    });
    setUpdateData(true);
    abrirFecharModalEditar();
  }).catch(error=>{
    console.log(error);
  })
}

//pedido delete

const pedidoDelete=async()=>{
  await axios.delete(baseUrl+"/"+alunoSelecionado.id)
  .then(response=>{
    setData(data.filter(aluno=>aluno.id !== response.data));
    setUpdateData(true);
    abrirFecharModalExcluir();
  }).catch(error=>{
    console.log(error);
  })
}




useEffect(()=>{
  if(updateData){
    setUpdateData(false);
    pedidoGet();
  }
},[updateData])


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
                <button className='btn btn-primary' onClick={()=>selecionarAluno(aluno,"Editar")}>Editar</button>
                <button className='btn btn-danger' onClick={()=>selecionarAluno(aluno, "Excluir")}>Excluir</button>
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

      <Modal isOpen={modalEditar}>
        <ModalHeader>Editar Aluno</ModalHeader>
        <ModalBody>
          <div className="form-group">
            <label>ID: </label>
            <input type="text" className="form-control" readOnly
              value={alunoSelecionado && alunoSelecionado.id} />
            <br />
            <label>Nome: </label><br />
            <input type="text" className="form-control" name="name" onChange={handleChange}
              value={alunoSelecionado && alunoSelecionado.name} /><br />
            <label>Email: </label><br />
            <input type="text" className="form-control" name="email" onChange={handleChange}
              value={alunoSelecionado && alunoSelecionado.email} /><br />
            <label>Idade: </label><br />
            <input type="text" className="form-control" name="idade" onChange={handleChange}
              value={alunoSelecionado && alunoSelecionado.idade} /><br />
          </div>
        </ModalBody>
        <ModalFooter>
          <button className="btn btn-primary" onClick={() => pedidoPut()}>Editar</button>{"  "}
          <button className="btn btn-danger" onClick={() => abrirFecharModalEditar()} >Cancelar</button>
        </ModalFooter>
      </Modal>

      <Modal isOpen={modalExcluir}>
        <ModalBody>
          Confirma a exclusão deste(a) aluno(a) : {alunoSelecionado && alunoSelecionado.name} ?
        </ModalBody>
        <ModalFooter>
          <button className="btn btn-danger" onClick={() => pedidoDelete()} > Sim </button>
          <button className="btn btn-secondary" onClick={() => abrirFecharModalExcluir()}> Não </button>
        </ModalFooter>
      </Modal>

    </div>
  );
}

export default App;
