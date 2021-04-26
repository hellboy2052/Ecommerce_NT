import React,{useState} from "react";
import Axios from "axios";
import {
  Table,
  Button,
 
} from "reactstrap";


const Category = (props) => {
  const Delete=(id)=> {
     Axios.delete(`https://hngtiendng.azurewebsites.net/api/Category/${id}`).then(
      (res) => {
        console.log(res);
        console.log(res.data);
      }
    );
  }
  const [modal, setModal] = useState(false);
  const [name1,setName]=useState("");
  const toggle = () => setModal(!modal);
  const Post=async()=>{
    var bodyFormData = new FormData();
    bodyFormData.append('name',)
    await Axios.post(

      `https://hngtiendng.azurewebsites.net/api/Category`,
      {
        name:name1
      }
      ) .then(res => {
        console.log(res);
        console.log(res.data);
      })
  }
  const setName1=(e)=>{
    setName(e.target.value)
  }

  return (
    <Table>
      <thead>
        <tr>
          <th>#</th>
          <th>Name</th>
          <th>Option</th>
          <th>
            <Button color="success" onClick={toggle}>Create</Button>
          </th>
          <createModal modal={modal} toggle={toggle} />
        </tr>
      </thead>
      {props.item.map(function (e, i) {
        return (
          <tbody>
            <tr key={i}>
              <th scope="row">{e.id}</th>
              <td>{e.name}</td>
              <td>
                <Button color="info">Update</Button>{" "}
                <Button color="danger" onClick={() => Delete(e.id)}>
                  Delete
                </Button>
              </td>
            </tr>
          </tbody>
        );
      })}
    </Table>
  );
};

export default Category;
