import { useState, useEffect } from "react";
import React from "react";
import { constants } from "../../Constants";
import { Modal, Button, Form, Row, Col, Table, InputGroup, FormControl } from "react-bootstrap";

function DesignThemeData() {
    // Design theme data
    const [designThemes, setDesignThemes] = useState([]);
    const [designTheme, setDesignTheme] = useState();

    const [addShow, setAddShow] = useState(false);
	const addHandleClose = () => setAddShow(false);
	const addHandleShow = () => setAddShow(true);

    const [editShow, setEditShow] = useState(false);
	const editHandleClose = () => setEditShow(false);
	const editHandleShow = () => setEditShow(true);

    const [search, setSearch] = useState("");
	const [filterParameters, setFilterParameters] = useState({
		BaseColor: false,
		SecondaryColor: false,
		AccentColor: false
	});

    // Get exercises data
	function getDesignThemes() {
		fetch(constants.API_URL + "DesignThemeData")
			.then(res => res.json())
			.then(data => setDesignThemes(data));
	}

	// onLoad function
	useEffect(() => {
		getDesignThemes();
	}, []);

    // Show add modal function
	function addModalShow() {
		addHandleShow();
		setDesignTheme({
			Id: undefined,
			BaseColor: "",
			SecondaryColor: "",
            AccentColor: ""
		});
	}

    // Show edit modal function
	function editModalShow(exercise) {
		editHandleShow();
		setDesignTheme(exercise);
	}

    // Delete item function
	function deleteClick(id) {
		if (window.confirm("Are you sure?")) {
			fetch(constants.API_URL + `DesignThemeData/${id}`, {
				method: "DELETE",
				headers: {
					Accept: "application/json",
					"Content-Type": "application/json"
				}
			})
				.then(res => res.json())
				.then(
					result => {
						getDesignThemes();
					},
					error => {
						alert("Failed");
					}
				);
		}
	}

    // Seact by color
	function searchColor(color) {
		let arr = [];
		for (let i = 0; i < designThemes.length; i++) {
			if (designThemes[i].BaseColor.match(color)
            || designThemes[i].SecondaryColor.match(color)
            || designThemes[i].AccentColor.match(color)) {
				arr.push(designThemes[i]);
			}
		}
		setDesignThemes(arr);
	}

    // Sort functions
	function sortBySection() {
		if (filterParameters.BaseColor) {
			designThemes.sort((a, b) => a.BaseColor.localeCompare(b.BaseColor));
			setFilterParameters({
				BaseColor: false,
				SecondaryColor: filterParameters.SecondaryColor,
				AccentColor: filterParameters.AccentColor
			});
		} else {
			designThemes.sort((b, a) => a.BaseColor.localeCompare(b.BaseColor));
			setFilterParameters({
				BaseColor: true,
				SecondaryColor: filterParameters.SecondaryColor,
				AccentColor: filterParameters.AccentColor
			});
		}
	}
}

export default DesignThemeData;