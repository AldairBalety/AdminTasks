
import React from 'react';

const TermsOfService: React.FC = () => {
  return (
    <div style={{ padding: '20px', maxWidth: '800px', margin: '0 auto' }}>
      <h1>Términos de Servicio - Digital Pulse</h1>
      <p><strong>Última actualización:</strong> {new Date().toLocaleDateString('es-ES')}</p>
      
      <h2>1. Aceptación de los Términos</h2>
      <p>Al interactuar con Digital Pulse en Facebook, aceptas estar sujeto a estos Términos de Servicio. Si no estás de acuerdo con estos términos, no utilices nuestros servicios.</p>

      <h2>2. Descripción del Servicio</h2>
      <p>Digital Pulse es un bot automatizado que:</p>
      <ul>
        <li>Genera contenido educativo e informativo sobre tecnología</li>
        <li>Publica noticias y artículos sobre innovación tecnológica</li>
        <li>Utiliza inteligencia artificial para crear contenido relevante</li>
        <li>Proporciona información actualizada sobre tendencias tecnológicas</li>
      </ul>

      <h2>3. Uso Aceptable</h2>
      <p>Al usar nuestros servicios, te comprometes a:</p>
      <ul>
        <li>No utilizar el servicio para fines ilegales o no autorizados</li>
        <li>No intentar interferir con el funcionamiento del bot</li>
        <li>Respetar los derechos de autor y propiedad intelectual</li>
        <li>No enviar spam o contenido malicioso</li>
        <li>Cumplir con las políticas de Facebook</li>
      </ul>

      <h2>4. Contenido Generado</h2>
      <p>El contenido generado por Digital Pulse:</p>
      <ul>
        <li>Es creado automáticamente usando inteligencia artificial</li>
        <li>Tiene fines informativos y educativos</li>
        <li>Puede contener errores o inexactitudes ocasionales</li>
        <li>No constituye asesoramiento profesional</li>
        <li>Está sujeto a derechos de autor y no debe ser reproducido sin permiso</li>
      </ul>

      <h2>5. Limitación de Responsabilidad</h2>
      <p>Digital Pulse no se hace responsable por:</p>
      <ul>
        <li>Exactitud completa del contenido generado automáticamente</li>
        <li>Decisiones tomadas basándose en nuestro contenido</li>
        <li>Interrupciones temporales del servicio</li>
        <li>Daños indirectos o consecuenciales</li>
      </ul>

      <h2>6. Propiedad Intelectual</h2>
      <p>Todo el contenido, diseño, código y materiales de Digital Pulse están protegidos por derechos de autor y otras leyes de propiedad intelectual.</p>

      <h2>7. Privacidad</h2>
      <p>El uso de nuestros servicios está sujeto a nuestra Política de Privacidad, que forma parte integral de estos términos.</p>

      <h2>8. Modificaciones</h2>
      <p>Nos reservamos el derecho de modificar estos términos en cualquier momento. Los cambios entrarán en vigor inmediatamente después de su publicación.</p>

      <h2>9. Terminación</h2>
      <p>Podemos suspender o terminar el acceso a nuestros servicios en cualquier momento, sin previo aviso, por violación de estos términos.</p>

      <h2>10. Ley Aplicable</h2>
      <p>Estos términos se regirán por las leyes aplicables en la jurisdicción donde operamos, sin conflicto con las disposiciones legales.</p>

      <h2>11. Contacto</h2>
      <p>Para preguntas sobre estos términos, contacta:</p>
      <ul>
        <li><strong>Email:</strong> legal@digitalpulse.com</li>
        <li><strong>Página de Facebook:</strong> Digital Pulse</li>
      </ul>

      <p><em>Al continuar usando nuestros servicios, confirmas tu aceptación de estos términos.</em></p>
    </div>
  );
};

export default TermsOfService;
