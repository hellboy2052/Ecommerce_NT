import React, { useEffect, useState } from "react";
import {
  Table,
  Button,
  Form,
  FormGroup,
  Label,
  Input,
  FormText,
  Modal,
  ModalHeader,
  ModalBody,
  ModalFooter,
} from "reactstrap";

import { useSelector, useDispatch } from "react-redux";
import { get_category_list } from "../../actions/category";
import { create_category } from "../../actions/category";
export default function CategoryList(props) {

 
  
  useEffect(() => {
    dispatch(get_category_list());
    //checkVar();
  },[]);
  const [modal, setModal] = useState(false);
  const [category, setCategory] = useState({});
  const dispatch = useDispatch();
  const toggle = () => setModal(!modal);
  const { categoryList } = useSelector((state) => state.category);
  


  const postCategory = async() => {
    dispatch(create_category(category))

  };
  var list_category = categoryList.data;

  
  const submit=()=>{

    postCategory();
    toggle()
  }
  return (
    <Table class="table">
      <thead>
        <tr>
          <th scope="col">#</th>
          <th scope="col">Name</th>
          <th scope="col">
            <Button color="success" onClick={toggle}>
              Create
            </Button>
          </th>
          <Modal isOpen={modal} toggle={toggle}>
            <ModalHeader toggle={toggle}>Product</ModalHeader>
            <ModalBody>
              <Form>
                <FormGroup>
                  <Label for="exampleEmail">Name</Label>
                  <Input
                    type="text"
                    name="name"
                    id="exampleEmail"
                    placeholder="Name"
                    onChange={(e)=>setCategory({...category,name:e.target.value})}
                  />
                
                  <FormText color="muted">
                    This is some placeholder block-level help text for the above
                    input. It's a bit lighter and easily wraps to a new line.
                  </FormText>
                </FormGroup>


              </Form>
            </ModalBody>
            <ModalFooter>
              <Button color="success" onClick={submit}>
                Create
              </Button>{" "}
              <Button color="danger" onClick={toggle}>
                Cancel
              </Button>
            </ModalFooter>
          </Modal>
        </tr>
      </thead>
      <tbody>
        {props.list &&
          props.list.map((item) => {
            return (
              <tr>
                <th scope="row">{item.id}</th>
                <td>{item.name}</td>
                
                <td>
                  <Button color="info">Update</Button>{" "}
                  <Button color="danger">Delete</Button>
                </td>
              </tr>
            );
          })}
      </tbody>
    </Table>
  );
}
