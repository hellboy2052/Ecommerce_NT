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
import { update_category } from "../../actions/category";
import { Link } from "react-router-dom";
export default function CategoryList(props) {

 
  
  useEffect(() => {
    dispatch(update_category());
    //checkVar();
  },[]);
  const [modal, setModal] = useState(false);
  
  const dispatch = useDispatch();
  const toggle = () => setModal(!modal);
  const { categoryList } = useSelector((state) => state.category);
  
  return (
    <Table class="table">
      <thead>
        <tr>
          <th scope="col">#</th>
          <th scope="col">Name</th>
          
          
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
                  
                  <Link to={`updateCategory/${item.id}`}><Button color="info" >Update</Button>{" "}</Link>
                </td>
              </tr>
            );
          })}
      </tbody>
    </Table>
  );
}
