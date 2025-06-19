
import React from 'react';

const DataDeletion: React.FC = () => {
  return (
    <div style={{ padding: '20px', maxWidth: '800px', margin: '0 auto' }}>
      <h1>Instrucciones de Eliminación de Datos - Digital Pulse</h1>
      <p><strong>Última actualización:</strong> {new Date().toLocaleDateString('es-ES')}</p>
      
      <h2>¿Qué datos recopilamos?</h2>
      <p>Digital Pulse recopila información limitada cuando interactúas con nuestro contenido en Facebook:</p>
      <ul>
        <li>ID público de Facebook</li>
        <li>Nombre público de Facebook</li>
        <li>Interacciones con nuestras publicaciones (likes, comentarios, shares)</li>
        <li>Datos de engagement para análisis de contenido</li>
      </ul>

      <h2>Cómo solicitar la eliminación de tus datos</h2>
      <p>Puedes solicitar la eliminación de tus datos personales de varias maneras:</p>

      <h3>Opción 1: Formulario de Eliminación de Datos</h3>
      <div style={{ background: '#f5f5f5', padding: '15px', borderRadius: '8px', margin: '10px 0' }}>
        <p><strong>Envía un email a:</strong> <a href="mailto:data-deletion@digitalpulse.com">data-deletion@digitalpulse.com</a></p>
        <p><strong>Incluye la siguiente información:</strong></p>
        <ul>
          <li>Tu nombre completo</li>
          <li>URL de tu perfil de Facebook</li>
          <li>Fecha aproximada de interacción con Digital Pulse</li>
          <li>Descripción específica de los datos que deseas eliminar</li>
        </ul>
      </div>

      <h3>Opción 2: Mensaje Directo en Facebook</h3>
      <div style={{ background: '#e3f2fd', padding: '15px', borderRadius: '8px', margin: '10px 0' }}>
        <p>Envía un mensaje directo a nuestra página de Facebook: <strong>Digital Pulse</strong></p>
        <p>Incluye en tu mensaje: "Solicito la eliminación de mis datos personales" junto con tu información de contacto.</p>
      </div>

      <h3>Opción 3: Formulario Web</h3>
      <div style={{ background: '#f3e5f5', padding: '15px', borderRadius: '8px', margin: '10px 0' }}>
        <p>Completa nuestro formulario online de eliminación de datos:</p>
        <p><strong>URL:</strong> <a href="https://digitalpulse.com/data-deletion" target="_blank" rel="noopener noreferrer">https://digitalpulse.com/data-deletion</a></p>
      </div>

      <h2>Proceso de Eliminación</h2>
      <ol>
        <li><strong>Recepción de Solicitud:</strong> Confirmaremos la recepción de tu solicitud en un plazo de 2 días hábiles</li>
        <li><strong>Verificación de Identidad:</strong> Podremos solicitar información adicional para verificar tu identidad</li>
        <li><strong>Procesamiento:</strong> Eliminaremos tus datos en un plazo de 15 días hábiles</li>
        <li><strong>Confirmación:</strong> Te enviaremos una confirmación una vez completada la eliminación</li>
      </ol>

      <h2>Qué datos NO podemos eliminar</h2>
      <p>Por razones legales o técnicas, algunos datos pueden no ser eliminables:</p>
      <ul>
        <li>Datos agregados y anonimizados utilizados para estadísticas</li>
        <li>Información requerida por ley para ser conservada</li>
        <li>Datos necesarios para resolver disputas legales activas</li>
        <li>Backups de seguridad (se eliminarán automáticamente según nuestro ciclo de retención)</li>
      </ul>

      <h2>Eliminación Automática</h2>
      <p>Además de las solicitudes manuales, nuestros sistemas eliminan automáticamente:</p>
      <ul>
        <li>Datos de interacción después de 2 años de inactividad</li>
        <li>Logs de sistema después de 90 días</li>
        <li>Datos temporales de análisis después de 6 meses</li>
      </ul>

      <h2>Derechos Adicionales</h2>
      <p>Además de la eliminación, también tienes derecho a:</p>
      <ul>
        <li><strong>Acceso:</strong> Solicitar una copia de tus datos personales</li>
        <li><strong>Rectificación:</strong> Corregir datos inexactos</li>
        <li><strong>Portabilidad:</strong> Recibir tus datos en formato estructurado</li>
        <li><strong>Limitación:</strong> Restringir el procesamiento de tus datos</li>
      </ul>

      <h2>Contacto y Soporte</h2>
      <div style={{ background: '#fff3cd', padding: '15px', borderRadius: '8px', margin: '20px 0' }}>
        <h3>Para consultas sobre eliminación de datos:</h3>
        <ul>
          <li><strong>Email:</strong> data-deletion@digitalpulse.com</li>
          <li><strong>Página de Facebook:</strong> Digital Pulse</li>
          <li><strong>Horario de atención:</strong> Lunes a Viernes, 9:00 AM - 6:00 PM</li>
          <li><strong>Tiempo de respuesta:</strong> 2-5 días hábiles</li>
        </ul>
      </div>

      <p><strong>Nota importante:</strong> Esta página cumple con los requisitos de Facebook para desarrolladores y las regulaciones de protección de datos (GDPR, CCPA).</p>
    </div>
  );
};

export default DataDeletion;
